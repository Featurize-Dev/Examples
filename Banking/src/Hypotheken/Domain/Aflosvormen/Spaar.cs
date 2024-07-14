using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;
public sealed class Spaar : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        throw new NotImplementedException();
    }
    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
        => 0;
}
