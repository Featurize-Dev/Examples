using Common.ValueObjects;

namespace Hypotheken.Domain;

public static class RestschuldExtension
{
    
}

public record RestSchuld(Amount Bruto, Amount Aflossing, Amount Netto)
{
    public static RestSchuld Create(Leningdeel leningdeel, DateOnly date)
        => Create(leningdeel, 
            (( date.Year - leningdeel.StartDatum.Year) * 12) + 
            date.Month - leningdeel.StartDatum.Month);

    public static RestSchuld Create(Leningdeel leningdeel, int termijn)
    {
        var t = leningdeel.Termijnen[..termijn];
        var sum_aflossingen = t.Sum(x => (decimal)x.Betaling);
        var sum_rente = t.Sum(x => (decimal)x.Rente);
        var hoofdsom = t.First().BeginStand + sum_rente;
        return new RestSchuld(hoofdsom, sum_aflossingen, t.Last().Eindstand);
    }

    public static RestSchuld OpDatum(Leningdeel leningdeel, DateOnly datum)
       => Create(leningdeel, datum);

    public static RestSchuld EindeTermijn(Leningdeel leningdeel, int termijn)
       => Create(leningdeel, termijn);

    public static RestSchuld EindeLooptijd(Leningdeel leningdeel)
        => Create(leningdeel, leningdeel.Looptijd);

    public static RestSchuld EindeRenteVastePeriode(Leningdeel leningdeel)
        => Create(leningdeel, leningdeel.RenteVastePeriode.Looptijd);
};
