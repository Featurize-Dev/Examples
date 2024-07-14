using Common.ValueObjects;
using System.Xml.Serialization;
using static Hypotheken.Domain.Kapitaal;
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
            var eindstand = Amount.Create(Math.Max((decimal)resterend - (decimal)aflossing, 0));
            var betaling = aflossing + rente;
            
            Add(new Termijn(resterend, rente, aflossing, betaling, eindstand));

            resterend = eindstand;
        }
    }
    public sealed record Termijn(Amount BeginStand, Amount Rente, Amount Aflossing, Amount Betaling, Amount Eindstand);
}

public class Kapitaal : List<Inleg>
{
    private readonly Leningdeel _leningdeel;

    private Kapitaal(Leningdeel leningdeel)
    {
        _leningdeel = leningdeel;
    }

    public static Kapitaal Create(Leningdeel leningdeel)
    {
        var k = new Kapitaal(leningdeel);
        k.Genereer();
        return k;
    }

    private void Genereer()
    {
        var kapitaal = Amount.Zero;

        for (int i = 1; i <= _leningdeel.Looptijd; i++)
        {
            var rente = kapitaal * _leningdeel.RenteVastePeriode.MaandRente;
            //var aflossing = _leningdeel.GetAflossing(rente, i);
            var inleg = _leningdeel.GetKapitaal(rente, i);
            var eindstand = Amount.Create(Math.Max((decimal)kapitaal + (decimal)inleg, 0));
            var betaling = inleg + rente;

            Add(new Inleg(kapitaal, rente, inleg, betaling, eindstand));

            kapitaal = eindstand;
        }
    }

    public sealed record Inleg(Amount BeginStand, Amount Rente, Amount MaandInleg, Amount Opbouw, Amount Eindstand);
}
