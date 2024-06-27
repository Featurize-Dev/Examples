using Common.ValueObjects;
using Hypotheken.Domain.Leningdelen;

namespace Hypotheken.Domain;


public interface Aflosvorm
{
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
}

public record Leningdeel(Aflosvorm LeningdeelType, DateOnly StartDatum, int Looptijd, RenteVastePeriode RenteVastePeriode, Amount Hoofdsom)
{
    public static Leningdeel Annuitear(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom) 
        => new(new Annuitair(), startDatum, looptijd, renteVastePeriode, hoofdsom);
    public static Leningdeel Lineair(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
        => new(new Lineair(), startDatum, looptijd, renteVastePeriode, hoofdsom);
    public static Leningdeel Aflossingsvrij(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom, decimal extraAflossing = 0)
        => new(new Aflossingsvrij(extraAflossing), startDatum, looptijd, renteVastePeriode, hoofdsom);

    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    
    public Amount GetAflossing(Amount resterend, int termijn)
        => LeningdeelType.GetAflossing(this, resterend, termijn);
}
