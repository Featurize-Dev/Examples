namespace Hypotheken.Domain;

public interface IleningdeelType
{
    RestSchuld GetRestschuld(Leningdeel leningdeel, int termijn);
}
