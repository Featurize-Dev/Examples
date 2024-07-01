using Common;
using Common.ValueObjects;
using FluentAssertions;
using Hypotheken.Domain;
using System.ComponentModel.DataAnnotations;

namespace Hypotheken.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var akte = new Hypotheek();

        akte.Hypotheekgevers.Add(new Hypotheekgever()
        {
            Voornaam = "Patrick",
            Achternaam = "Evers",
            Tussenvoegsel = string.Empty,
            BurgerServiceNummer = BSN.Parse("128.328.559"),
            Geboortedatum = DateOnly.Parse("1978-10-09"),
        });

        var rvp = RenteVastePeriode.Create(1.04m, 120);

        var leningdeel = Leningdeel.CreateAflossingsvrij(new(2024,1,1), 360, rvp, 150_000);

        var pTest1 = Percentage.Create(1.04m);
        var pTest = Percentage.Parse("1,04%");

        Percentage test = 1.04m;
        var hmm = Percentage.Parse("1%");

        pTest1.Equals(pTest);
        
        //akte.Lening.Leningdelen.Add(leningdeel);
    }


    [Fact]
    public void TermijnenTests()
    {
        var renteVastePeriode = new RenteVastePeriode(1.04m, 120);

        var aflossingsvrij = Leningdeel.CreateAflossingsvrij(new(2024, 1, 1), 360, renteVastePeriode, 150_000);

        var annuitair = Leningdeel.CreateAnnuitear(new(2024, 1, 1), 360, renteVastePeriode, 150_000);

        var lineair = Leningdeel.CreateLineair(new(2024, 1, 1), 360, renteVastePeriode, 150_000);

        //var termijnen = new Termijnen(annuitair);

        var rest =  RestSchuld.Create(aflossingsvrij, 120);
        
        var r1 = RestSchuld.EindeLooptijd(lineair);
        var r2 = RestSchuld.EindeLooptijd(aflossingsvrij);
        var r3 = RestSchuld.EindeLooptijd(annuitair);

        var r1a = RestSchuld.EindeRenteVastePeriode(lineair);
        var r2a = RestSchuld.EindeRenteVastePeriode(aflossingsvrij);
        var r3a = RestSchuld.EindeRenteVastePeriode(annuitair);

        var date = DateOnly.FromDateTime(DateTime.Now.AddMonths(-(120 - 24)));

        var rvp = new RenteVastePeriode(5m, 120);

        var ld = Leningdeel.CreateAflossingsvrij(date, 360, rvp, 100_000);

        var result = BoeteRente.Oversluiten(ld, RenteVastePeriode.Create(3, 120), DateOnly.FromDateTime(DateTime.Now));

        var woning = OnderpandFactory.Eengezinswoning(Energielabel.F, 250_000);

        var lening = new Lening();

        lening.Add(ld);
        
        var hypotheek = new Hypotheek()
        {
            Hypotheekgevers = [
                new Hypotheekgever() {
                        
                    }
                ],
            Hypotheeknemer = Hypotheeknemer.GoldCreditBank,
            Lening = lening,
            Onderpand = OnderpandFactory.Eengezinswoning(Energielabel.F, 250_000)
        };


    }

    [Fact]
    public void BoeteRente_Berekenen()
    {
        var rvp = RenteVastePeriode.Create(4, 24);
        var date = DateTime.Now.AddMonths(-rvp.Looptijd);
        var leningdeel = Leningdeel.CreateLineair(DateOnly.FromDateTime(date), 300, rvp, 150_000);

        var new_rvp = RenteVastePeriode.Create(2, 120);
        var boete = (decimal)BoeteRente.Oversluiten(leningdeel, new_rvp, DateOnly.FromDateTime(date));

        boete.Should().BeApproximately(5400, 1);
    }
} 