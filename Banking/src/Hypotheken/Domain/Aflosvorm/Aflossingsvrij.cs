using Common.ValueObjects;

namespace Hypotheken.Domain.Leningdelen;

public class Aflossingsvrij(decimal MaandelijkseAflossing) : Aflosvorm
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount restschuld, int termijn)
    {
        return MaandelijkseAflossing;
    }
}
