using Common.ValueObjects;
using Hypotheken.Domain.Aflosvormen;

namespace Hypotheken.Domain;


public interface Aflosvorm
{
    Percentage Boetevrij { get; }
    Amount GetAflossing(Leningdeel leningdeel, Amount rente, int termijn);
    Amount GetKapitaal(Leningdeel leningdeel, Amount rente, int termijn);
}

public record Leningdeel(Aflosvorm AflostVorm, DateOnly StartDatum, int Looptijd, RenteVastePeriode RenteVastePeriode, Amount Hoofdsom)
{
    public static Leningdeel CreateAnnuitear(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom)
        => LeningdeelBuilder
            .Create()
            .WithAflosvorm(new Annuitair())
            .WithStartDatum(startDatum)
            .WithLooptijd(looptijd)
            .WithRenteVastePeriode(renteVastePeriode)
            .WithHoofdsom(hoofdsom)
            .Build();

    public static Leningdeel CreateLineair(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom)
        => LeningdeelBuilder
            .Create()
            .WithAflosvorm(new Lineair())
            .WithStartDatum(startDatum)
            .WithLooptijd(looptijd)
            .WithRenteVastePeriode(renteVastePeriode)
            .WithHoofdsom(hoofdsom)
            .Build();

    public static Leningdeel CreateAflossingsvrij(DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, decimal hoofdsom, decimal extraAflossing = 0)
        => LeningdeelBuilder
            .Create()
            .WithAflosvorm(new Aflossingsvrij(extraAflossing))
            .WithStartDatum(startDatum)
            .WithLooptijd(looptijd)
            .WithRenteVastePeriode(renteVastePeriode)
            .WithHoofdsom(hoofdsom)
            .Build();


    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    
    public Amount GetAflossing(Amount rente, int termijn)
        => AflostVorm.GetAflossing(this, rente, termijn);

    public Amount GetKapitaal(Amount rente, int termijn)
        => AflostVorm.GetKapitaal(this, rente, termijn);
}
