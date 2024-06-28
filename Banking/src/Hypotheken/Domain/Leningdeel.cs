using Common.ValueObjects;

namespace Hypotheken.Domain;


public interface Aflosvorm
{
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
}

public record Leningdeel(Aflosvorm LeningdeelType, DateOnly StartDatum, int Looptijd, RenteVastePeriode RenteVastePeriode, Amount Hoofdsom)
{
    public static Leningdeel Annuitear(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom)
        => LeningdeelBuilder
            .Create()
            .Annuitear(startDatum, looptijd, renteVastePeriode, hoofdsom);

    public static Leningdeel Lineair(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom)
        => LeningdeelBuilder
            .Create()
            .Lineair(startDatum, looptijd, renteVastePeriode, hoofdsom);

    public static Leningdeel Aflossingsvrij(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom, decimal extraAflossing = 0)
        => LeningdeelBuilder
            .Create()
            .Aflossingsvrij(startDatum, looptijd, renteVastePeriode, hoofdsom, extraAflossing);


    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    
    public Amount GetAflossing(Amount resterend, int termijn)
        => LeningdeelType.GetAflossing(this, resterend, termijn);
}
