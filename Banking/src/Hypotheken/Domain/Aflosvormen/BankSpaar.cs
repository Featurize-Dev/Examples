using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;
public sealed class BankSpaar(Percentage spaarRente) : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Percentage SpaarRente { get; } = spaarRente;

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        var hoofdsom = (double)leningdeel.Hoofdsom;
        var maandSpaarRente = (double)SpaarRente / 12;
        double factor = Math.Pow(1 + (double)maandSpaarRente, termijn);
        
        double maandelijkseInleg = (hoofdsom * maandSpaarRente) / (factor - 1);

        return Amount.Create(maandelijkseInleg, leningdeel.Hoofdsom.Currency);
    }
}
