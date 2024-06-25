using Common.ValueObjects;

namespace Hypotheken.Domain;

public interface ILeningdeelType
{
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
}
