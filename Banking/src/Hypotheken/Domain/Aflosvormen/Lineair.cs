using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;

public sealed class Lineair : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return leningdeel.Hoofdsom / leningdeel.Looptijd;
    }

    public Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn)
        =>0;
}
