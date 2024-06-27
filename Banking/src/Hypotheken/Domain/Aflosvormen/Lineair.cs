using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;

public sealed class Lineair : Aflosvorm
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return leningdeel.Hoofdsom / leningdeel.Looptijd;
    }
}
