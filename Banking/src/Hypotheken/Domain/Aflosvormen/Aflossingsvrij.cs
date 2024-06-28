using Common.ValueObjects;

namespace Hypotheken.Domain.Aflosvormen;

public sealed class Aflossingsvrij(decimal MaandelijkseAflossing) : Aflosvorm
{
    public Percentage Boetevrij
        => Percentage.Create(10);

    public Amount GetAflossing(Leningdeel leningdeel, Amount restschuld, int termijn)
    {
        return MaandelijkseAflossing;
    }
}
