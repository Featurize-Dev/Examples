using Common.ValueObjects;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hypotheken.Domain;

public class Termijnen : List<Termijn>
{
    private readonly Leningdeel _leningdeel;

    public Termijnen(Leningdeel leningdeel)
    {
        _leningdeel = leningdeel;
        Genereer();
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
}

public sealed record Termijn(Amount BeginStand, Amount Rente, Amount Aflossing)
{
    public Amount Eindstand => Amount.Create(Math.Max((decimal)BeginStand - (decimal)Aflossing, 0), BeginStand.Currency);
    public Amount Betaling => Aflossing + Rente;
}
