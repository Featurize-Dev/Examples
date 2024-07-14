using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;
public sealed class BankSpaar(Percentage spaarRente) : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Percentage SpaarRente { get; } = spaarRente;
    
    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
        => 0m;

    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
    {
        double hoofdsom = (double)leningdeel.Hoofdsom;
        double maandRenteSpaar = (double)SpaarRente / 12;
        double factor = Math.Pow(1 + maandRenteSpaar, leningdeel.Looptijd);
        double maandelijkseInleg = (hoofdsom * maandRenteSpaar) / (factor - 1);

        return Amount.Create(maandelijkseInleg);
    }
}
