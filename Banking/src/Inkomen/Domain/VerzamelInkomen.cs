using Featurize.DomainModel;
using Inkomen.Domain.Events;

namespace Inkomen.Domain;

public class VerzamelInkomen : AggregateRoot<Guid>
{
    private readonly Dictionary<Guid, string> _inkomens = [];
    private VerzamelInkomen(Guid id) : base(id)
    {
    }
    public IReadOnlyDictionary<Guid, string> Inkomens => _inkomens;

    public static VerzamelInkomen Create()
    {
        var aggregate = new VerzamelInkomen(Guid.NewGuid());
        aggregate.RecordEvent(new VerzamelInkomenGestart());
        return aggregate;
    }

    public void AddInkomen(IInkomen inkomen)
    {
        RecordEvent(new InkomenToegevoegd(inkomen.Type, inkomen.Id));
    }

    private void Apply(VerzamelInkomenGestart e) 
    {
    }

    private void Apply(InkomenToegevoegd e)
    {
        _inkomens[e.InkomenId] = e.Type;
    }
}

public record InkomenToegevoegd(string Type, Guid InkomenId) : EventRecord;