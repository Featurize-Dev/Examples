using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Lening
{
    private Currency _currency;
    private List<Leningdeel> _leningdelen = [];
    public IReadOnlyList<Leningdeel> Leningdelen => _leningdelen.AsReadOnly();

    public static Lening Empty() => new();

    public void Add(Leningdeel leningdeel)
    {
        if (!_leningdelen.Any())
        {
            _currency = leningdeel.Hoofdsom.Currency;
        }

        if(leningdeel.Hoofdsom.Currency != _currency)
        {
            throw new ArgumentException("Leningdeel heeft niet de zelfde Currency.");
        }

        _leningdelen.Add(leningdeel);
    }

    public Amount Totaal => Amount.Create(Leningdelen.Sum(x => 
        (decimal)x.Hoofdsom), 
        Leningdelen.First().Hoofdsom.Currency);
}
