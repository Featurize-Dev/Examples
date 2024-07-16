using Featurize.DomainModel;

namespace Inkomen.Domain;

public class Loondienst : AggregateRoot<Guid>, IInkomen
{
    public Werknemer Werknemer { get; private set; } = Werknemer.Empty();
    public Werkgever Werkgever { get; private set; } = Werkgever.Empty();
    public string FunctieTitel { get; private set; } = string.Empty;
    public Overeenkomst DuurOvereenkomst { get; private set; } = Overeenkomst.BepaaldeTijd;
    public DateOnly IngangsDatum { get; private set; } = new(1990, 1, 1);
    public Amount Salaris { get; private set; } = 0m;
    public Amount ToetsInkomen { get; private set; } = 0m;
    public Arbeidsvoorwaarden Arbeidsvoorwaarden { get; private set; } = Arbeidsvoorwaarden.Empty();

    public string Type => "Loondienst";

    private Loondienst(Guid id) : base(id)
    {
    }

    public static Loondienst Create(Werknemer werknemer, Werkgever werkgever, Amount salaris, DateOnly ingangsDatum)
    {
        var loondienst = new Loondienst(Guid.NewGuid());
        loondienst.RecordEvent(new LoondienstAangemaakt(werknemer, werkgever, salaris, ingangsDatum));
        return loondienst;
    }

    internal void Apply(LoondienstAangemaakt e)
    {
        Werknemer = e.Werknemer;
        Werkgever = e.Werkgever;
        Salaris = e.Salaris;
        IngangsDatum = e.IngangsDatum;
    }
}

public record LoondienstAangemaakt(Werknemer Werknemer, Werkgever Werkgever, Amount Salaris, DateOnly IngangsDatum) : EventRecord;