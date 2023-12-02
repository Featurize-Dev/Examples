using Confluent.Kafka;
using Featurize.DomainModel;
using Featurize.Repositories;
using System.Reflection;
using System.Text.Json;

namespace CommonFeatures.Storage;
public class AggregateManager<TAggregate, TId>(
    IEntityRepository<PersistendEvent<TId>, Guid> repository, 
    IProducer<TId, PersistendEvent<TId>> eventProducer,
    IProducer<TId, TAggregate> snapshotProducer)

    where TAggregate : AggregateRoot<TAggregate, TId>
    where TId : struct, IEquatable<TId>
{
    private const string _applyMethodName = "Apply";

    public async Task<TAggregate?> LoadAsync(TId id)
    {
        var events = await repository.Query
            .Where(x => x.AggregateId.Equals(id))
            .OrderBy(x => x.Version)
            .Select(x => AggregateManager<TAggregate, TId>.GetEvent(x))
            .ToArrayAsync();


        if (events.Length == 0)
        {
            return null;
        }

        var aggregate = AggregateRoot.Create<TAggregate, TId>(id);
        var eventCollection = EventCollection.Create(id, events);
        aggregate.LoadFromHistory(eventCollection);
        return aggregate;
    }

    public async Task SaveAsync(TAggregate aggregate)
    {
        var events = aggregate.GetUncommittedEvents();
        var version = aggregate.Version;
        var aggregateName = typeof(TAggregate).Name;

        foreach (var e in events)
        {
            version++;
            var evnt = CreatePresistedEvent(aggregate, aggregateName, version, e);
            await repository.SaveAsync(evnt);
            await eventProducer.ProduceAsync($"{aggregateName}.events", new()
            {
                Key = aggregate.Id,
                Value = evnt,
            });
        }

        await snapshotProducer.ProduceAsync($"{aggregateName}.snapshot", new()
        {
            Key = aggregate.Id,
            Value = aggregate,
        });
    }

    private static PersistendEvent<TId> CreatePresistedEvent(TAggregate aggregate, string aggregateName, int version, EventRecord e)
    {
        return new PersistendEvent<TId>(
                       Guid.NewGuid(),
                       aggregateName,
                       aggregate.Id,
                       version,
                       e.GetType().Name,
                       JsonSerializer.Serialize(e, e.GetType(), JsonSerializerOptions.Default)
                   );
    }

    private static EventRecord GetEvent(PersistendEvent<TId> x)
    {
        var eventType = AggregateManager<TAggregate, TId>.GetEventType(x.EventName);

        var e = JsonSerializer.Deserialize(x.Payload, eventType);
        if (e as EventRecord is null)
        {
            throw new InvalidCastException();
        }

        return (EventRecord)e;
    }

    private static Type GetEventType(string eventName)
    {
        var aggregateType = typeof(TAggregate);
        var methods = aggregateType
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
            .Where(x => x.Name == _applyMethodName);

        var eventTypes = methods.Select(x => x.GetParameters()[0]);

        var eventType = eventTypes.FirstOrDefault(x => x.ParameterType.Name == eventName)?.ParameterType;

        return eventType
            ?? throw new InvalidOperationException($"Can not process event '{eventName}'");
    }
}

public record PersistendEvent<TId>(Guid Id, string AggregateName, TId AggregateId, int Version, string EventName, string Payload) : IIdentifiable<PersistendEvent<TId>, Guid>
{
    public static Guid Identify(PersistendEvent<TId> entity)
    {
        return entity.Id;
    }
}
