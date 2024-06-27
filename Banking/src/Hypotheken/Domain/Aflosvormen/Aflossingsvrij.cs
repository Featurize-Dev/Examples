using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;

public sealed class Aflossingsvrij(decimal MaandelijkseAflossing) : Aflosvorm
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount restschuld, int termijn)
    {
        return MaandelijkseAflossing;
    }
}
