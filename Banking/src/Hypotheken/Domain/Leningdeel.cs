using Common.ValueObjects;
using Hypotheken.Domain.Leningdelen;

namespace Hypotheken.Domain;


public interface ILeningdeelType
{
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
}

public record Leningdeel(ILeningdeelType LeningdeelType, int Looptijd, RenteVastePeriode RenteVastePeriode, Amount Hoofdsom)
{
    public static Leningdeel Annuitear(int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom) 
        => new(new Annuitair(), looptijd, renteVastePeriode, hoofdsom);
    public static Leningdeel Lineair(int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
        => new(new Lineair(), looptijd, renteVastePeriode, hoofdsom);
    public static Leningdeel Aflossingsvrij(int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom, decimal extraAflossing = 0)
        => new(new Aflossingsvrij(extraAflossing), looptijd, renteVastePeriode, hoofdsom);

    public DateOnly StartDatum => RenteVastePeriode.StartDatum;
    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    
    public Amount GetAflossing(Amount resterend, int termijn)
        => LeningdeelType.GetAflossing(this, resterend, termijn);
}
