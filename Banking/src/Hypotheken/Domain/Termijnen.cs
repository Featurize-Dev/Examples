using Common.ValueObjects;
using static Hypotheken.Domain.Termijnen;

namespace Hypotheken.Domain;

public class Termijnen : List<Termijn>
{
    private readonly Leningdeel _leningdeel;

    private Termijnen(Leningdeel leningdeel)
    {
        _leningdeel = leningdeel;
    }

    public static Termijnen Create(Leningdeel leningdeel)
    {
        var termijn = new Termijnen(leningdeel);
        termijn.Genereer();
        return termijn;
    }

    private void Genereer()
    {
        var resterend = _leningdeel.Hoofdsom;

        for (int i = 1; i <= _leningdeel.Looptijd; i++)
        {
            var rente = resterend * _leningdeel.RenteVastePeriode.MaandRente;
            var aflossing = _leningdeel.GetAflossing(rente, i);
            var termijn = new Termijn(resterend, rente, aflossing);
            resterend = termijn.Eindstand;
            Add(termijn);
        }
    }
    public sealed record Termijn(Amount BeginStand, Amount Rente, Amount Aflossing)
    {
        public Amount Eindstand => Amount.Create(Math.Max((decimal)BeginStand - (decimal)Aflossing, 0), BeginStand.Currency);
        public Amount Betaling => Aflossing + Rente;
    }
}


