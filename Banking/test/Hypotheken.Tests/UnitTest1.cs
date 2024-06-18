using Common;
using Common.ValueObjects;
using Hypotheken.Domain;
using System.ComponentModel.DataAnnotations;

namespace Hypotheken.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var akte = new HypotheekAkte();

        akte.Hypotheekgevers.Add(new Hypotheekgever()
        {
            Voornaam = "Patrick",
            Achternaam = "Evers",
            Tussenvoegsel = string.Empty,
            Burgerservicenummer = BSN.Parse("128.328.559"),
            Geboortedatum = DateOnly.Parse("1978-10-09"),
            PII = new()
        });

        var leningdeel = Leningdeel.Aflossingsvrij(new(2024, 1, 1), 360, 120, 1.04m, 150_000);

        var pTest1 = Percentage.Create(1.04m);
        var pTest = Percentage.Parse("1,04%");

        Percentage test = 1.04m;
        var hmm = (decimal)Percentage.Parse("1%");

        pTest1.Equals(pTest);
        
        akte.Lening.Leningdelen.Add(leningdeel);
    }
} 