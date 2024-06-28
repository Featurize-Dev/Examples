using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;
public sealed class BankSpaar : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return 0;
    }
}
