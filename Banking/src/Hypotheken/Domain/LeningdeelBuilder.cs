using Common.ValueObjects;
using Hypotheken.Domain.Aflosvormen;

namespace Hypotheken.Domain;

public sealed class LeningdeelBuilder
{
    private Amount _hoofdsom = 0;
    private DateOnly _startdatum = new(2024, 1, 1);
    private int _looptijd = 0;
    private RenteVastePeriode _rvp = RenteVastePeriode.Create(0, 0);
    private Aflosvorm _aflosvorm = new Aflossingsvrij(0);

    private LeningdeelBuilder() { }

    public static LeningdeelBuilder Create()
        => new();

    public LeningdeelBuilder WithStartDatum(DateOnly dateOnly)
    {
        _startdatum = dateOnly;
        return this;
    }

    public LeningdeelBuilder WithHoofdsom(Amount hoofdsom)
    {
        _hoofdsom = hoofdsom;
        return this;
    }

    public LeningdeelBuilder WithAflosvorm(Aflosvorm aflosvorm)
    {
        _aflosvorm = aflosvorm;
        return this;
    }

    public LeningdeelBuilder WithRenteVastePeriode(Percentage rente, int looptijd)
    {
        _rvp = RenteVastePeriode.Create(rente, looptijd);
        return this;
    }

    public LeningdeelBuilder WithRenteVastePeriode(RenteVastePeriode renteVastePeriode)
    {
        _rvp = renteVastePeriode;
        return this;
    }

    public LeningdeelBuilder WithLooptijd(int looptijd)
    {
        _looptijd = looptijd;
        return this;
    }

    public Leningdeel Build()
        => new(_aflosvorm, _startdatum, _looptijd, _rvp, _hoofdsom);
}
