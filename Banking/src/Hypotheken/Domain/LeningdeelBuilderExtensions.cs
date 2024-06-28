using Common.ValueObjects;
using Hypotheken.Domain.Aflosvormen;

namespace Hypotheken.Domain;

public static class LeningdeelBuilderExtensions
{
    public static Leningdeel Annuitear(this LeningdeelBuilder builder, DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
        => builder
        .WithAflosvorm(new Annuitair())
        .WithStartDatum(startDatum)
        .WithLooptijd(looptijd)
        .WithRenteVastePeriode(renteVastePeriode)
        .WithHoofdsom(hoofdsom)
        .Build();

    public static Leningdeel Lineair(this LeningdeelBuilder builder, DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom)
        => builder
        .WithAflosvorm(new Lineair())
        .WithStartDatum(startDatum)
        .WithLooptijd(looptijd)
        .WithRenteVastePeriode(renteVastePeriode)
        .WithHoofdsom(hoofdsom)
        .Build();

    public static Leningdeel Aflossingsvrij(this LeningdeelBuilder builder, DateOnly startDatum, int looptijd, RenteVastePeriode renteVastePeriode, Amount hoofdsom, decimal extraAflossing = 0)
        => builder
        .WithAflosvorm(new Aflossingsvrij(extraAflossing))
        .WithStartDatum(startDatum)
        .WithLooptijd(looptijd)
        .WithRenteVastePeriode(renteVastePeriode)
        .WithHoofdsom(hoofdsom)
        .Build();
}