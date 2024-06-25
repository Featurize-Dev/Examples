using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Lineair : ILeningdeelType
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        return leningdeel.Hoofdsom / leningdeel.Looptijd;
    }
}
