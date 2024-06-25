using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Aflossingsvrij(decimal MaandelijkseAflossing) : ILeningdeelType
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount restschuld, int termijn)
    {
        return MaandelijkseAflossing;
    }
}
