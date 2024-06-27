using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;
public sealed class Leven : Aflosvorm
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        throw new NotImplementedException();
    }
}
