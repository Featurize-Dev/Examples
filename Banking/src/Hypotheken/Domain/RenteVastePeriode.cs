using Common.ValueObjects;

namespace Hypotheken.Domain;

public sealed record RenteVastePeriode(Percentage Rente, int Looptijd)
{
    public static RenteVastePeriode Create(Percentage rente, int looprijd) 
        => new(rente, looprijd);

    public Percentage MaandRente => Rente / 12;

}