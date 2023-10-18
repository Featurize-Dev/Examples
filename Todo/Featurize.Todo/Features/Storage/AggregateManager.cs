using Featurize.DomainModel;
using Featurize.Repositories;
using System.Text.Json;

namespace Featurize.Todo.Features.Storage;

public class AggregateManager<TAggregate, TId>
    where TAggregate : AggregateRoot<TAggregate, TId>
    where TId : struct, IEquatable<TId>
{
    private const string ApplyMethodName = "Apply";
    private readonly IEntityRepository<PersistendEvent<TId>, Guid> _repository;

    public AggregateManager(IEntityRepository<PersistendEvent<TId>, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<TAggregate?> LoadAsync(TId id)
    {
        var events = await _repository.Query
            .Where(x=>x.AggregateId.Equals(id))
            .OrderBy(x => x.Version)
            .Select(x => GetEvent(x))
            .ToArrayAsync();

        
        if (!events.Any())
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
        foreach (var e in events)
        {
            version++;
             await _repository.SaveAsync(new PersistendEvent<TId>(
                Guid.NewGuid(),
                typeof(TAggregate).Name,
                aggregate.Id,
                version,
                e.GetType().Name,
                JsonSerializer.Serialize(e)
            ));
        }
    }

    private EventRecord GetEvent(PersistendEvent<TId> x)
    {
        var eventType = GetEventType(x.EventName);
       
        var e = JsonSerializer.Deserialize(x.Payload, eventType);
        if(e as EventRecord is null)
        {
            throw new InvalidCastException();
        }

        return (EventRecord)e;
    }

    private Type GetEventType(string eventName)
    {
        var aggregateType = typeof(TAggregate);
        var methods = aggregateType
            .GetMethods()
            .Where(x => x.Name == ApplyMethodName);

        var eventTypes = methods.Select(x => x.GetParameters()[0]);

        var eventType = eventTypes.FirstOrDefault(x => x.ParameterType.Name == eventName)?.ParameterType;

        if (eventType == null)
        {
            throw new InvalidOperationException($"Can not process event '{eventName}'");
        }

        return eventType;
    }
}

public record PersistendEvent<TId>(Guid Id, string AggregateName, TId AggregateId, int Version, string EventName, string Payload) : IIdentifiable<PersistendEvent<TId>, Guid>
{
    public static Guid Identify(PersistendEvent<TId> entity)
    {
        return entity.Id;
    }
}
