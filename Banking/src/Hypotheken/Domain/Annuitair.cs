using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Annuitair : ILeningdeelType
{
    public Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn)
    {
        var hypotheek = (double)leningdeel.Hoofdsom;
        var maandrente = (double)leningdeel.RenteVastePeriode.MaandRente;
        
        var annuiteit = maandrente / (1 - Math.Pow(1 + maandrente, -leningdeel.Looptijd)) * hypotheek;

        return (decimal)annuiteit - rente;
    }
}
