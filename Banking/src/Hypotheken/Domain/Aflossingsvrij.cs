namespace Hypotheken.Domain;

public class Aflossingsvrij(decimal MaandelijkseAflossing) : IleningdeelType
{
    public RestSchuld GetRestschuld(Leningdeel leningdeel, int termijn)
    {
        var restschuld = (decimal)leningdeel.Bedrag;
        var maandelijkseRente = (decimal)leningdeel.Rente / 12;

        for (int i = 0; i < termijn; i++)
        {
            // Rente berekenen over de huidige restschuld
            decimal rente = (decimal)leningdeel.Bedrag * maandelijkseRente;
            // Nieuwe restschuld na aflossing en rentebetaling
            restschuld = restschuld + rente - MaandelijkseAflossing;
            if (restschuld < 0) restschuld = 0;
        }

        var bruto = (decimal)leningdeel.Bedrag;
        var aflossing = MaandelijkseAflossing * termijn;
        var netto = (decimal)leningdeel.Bedrag - aflossing;
        return new(bruto, aflossing, netto);
    }
}
