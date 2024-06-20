namespace Hypotheken.Domain;

public class Lineair : IleningdeelType
{
    public RestSchuld GetRestschuld(Leningdeel leningdeel, int termijn)
    {
        var maandelijkseAflossing = leningdeel.Hoofdsom
    }
}
