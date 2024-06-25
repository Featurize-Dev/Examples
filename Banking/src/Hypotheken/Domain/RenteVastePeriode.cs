using Common.ValueObjects;

namespace Hypotheken.Domain;

public sealed record RenteVastePeriode(DateOnly StartDatum, Percentage Rente, int Looptijd)
{
    public static RenteVastePeriode Create(Percentage rente, int looprijd) 
        => new(DateOnly.FromDateTime(DateTime.Now), rente, looprijd);

    public DateOnly EindDatum => StartDatum.AddMonths(Looptijd);
    public Percentage MaandRente => Rente / 12;

}