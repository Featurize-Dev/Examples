using Common.ValueObjects;

namespace Hypotheken.Domain;

public sealed record BoeteRente(Leningdeel Leningdeel, Amount Boete)
{
    public static BoeteRente Oversluiten(Leningdeel leningdeel, RenteVastePeriode renteVastePeriode)
    {
        var diff = (leningdeel.RenteVastePeriode.Rente - renteVastePeriode.Rente) / 12;
        var maanden = ((leningdeel.RenteVastePeriode.EindDatum.Year - renteVastePeriode.StartDatum.Year) * 12)
            + leningdeel.RenteVastePeriode.EindDatum.Month - renteVastePeriode.StartDatum.Month;

        var restschuld = RestSchuld.OpDatum(leningdeel, renteVastePeriode.StartDatum);
        var boete = (restschuld.Netto * diff) * maanden;

        return new(
            new Leningdeel(
            leningdeel.LeningdeelType, 
            leningdeel.Looptijd,
            renteVastePeriode,
            restschuld.Netto),
            boete);
    }


}

public sealed record RenteVastePeriode(DateOnly StartDatum, Percentage Rente, int Looptijd)
{
    public static RenteVastePeriode Create(Percentage rente, int looprijd) 
        => new(DateOnly.FromDateTime(DateTime.Now), rente, looprijd);

    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    public Percentage MaandRente => Rente / 12;

}