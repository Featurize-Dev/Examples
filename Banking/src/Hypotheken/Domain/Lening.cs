using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Lening
{
    private List<Leningdeel> _leningdelen = [];
    public IReadOnlyList<Leningdeel> Leningdelen => _leningdelen.AsReadOnly();

    public static Lening Empty() => new();

    public void Add(Leningdeel leningdeel)
    {
        _leningdelen.Add(leningdeel);
    }

    public Amount Totaal => Amount.Create(Leningdelen.Sum(x => (decimal)x.Hoofdsom));
}
