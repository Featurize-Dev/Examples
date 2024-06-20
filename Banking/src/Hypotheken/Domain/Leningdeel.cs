using Common.ValueObjects;

namespace Hypotheken.Domain;

public record Leningdeel(IleningdeelType leningdeelType, DateOnly StartDatum, int Looptijd, int RenteVastePeriode, Percentage Rente, decimal Hoofdsom)
{
    public static Leningdeel Annuitear(DateOnly startDatum, int looptijd, int renteVastePeriode, Percentage rente, Amount bedrag) => new(new Annuitair(), startDatum, looptijd, renteVastePeriode, rente, bedrag);
    public static Leningdeel Lineair(DateOnly startDatum, int looptijd, int renteVastePeriode, Percentage rente, Amount bedrag) => new(new Lineair(), startDatum, looptijd, renteVastePeriode, rente, bedrag);
    public static Leningdeel Aflossingsvrij(DateOnly startDatum, int looptijd, int renteVastePeriode, Percentage rente, Amount bedrag, decimal extraAflossing = 0) => 
        new(new Aflossingsvrij(extraAflossing), startDatum, looptijd, renteVastePeriode, rente, bedrag);

    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    public DateOnly EindDatumRenteVastePeriode => StartDatum.AddMonths(RenteVastePeriode);

    public RestSchuld GetRestschuld(int termijn) => 
        leningdeelType.GetRestschuld(this, termijn);
}
