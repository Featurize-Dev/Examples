using Common.ValueObjects;

namespace Hypotheken.Domain;

public sealed record BoeteRente(Leningdeel Leningdeel, Amount Boete)
{
    public static BoeteRente Oversluiten(Leningdeel leningdeel, RenteVastePeriode renteVastePeriode, DateOnly ingangsDatum)
    {
        var einddatum = leningdeel.StartDatum.AddMonths(renteVastePeriode.Looptijd);

        var diff = (leningdeel.RenteVastePeriode.Rente - renteVastePeriode.Rente) / 12;
        var maanden = ((einddatum.Year - ingangsDatum.Year) * 12)
            + einddatum.Month - ingangsDatum.Month;

        var restschuld = RestSchuld.OpDatum(leningdeel, leningdeel.StartDatum);
        var boete = (restschuld.Netto * diff) * maanden;

        return new(
            new Leningdeel(
            leningdeel.LeningdeelType,
            ingangsDatum,
            leningdeel.Looptijd,
            renteVastePeriode,
            restschuld.Netto),
            boete);
    }
}
