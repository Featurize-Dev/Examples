using Common.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests;

public class Amount_Tests
{
    [Fact]
    public void Parsing()
    {
        Currency.Default = Currency.Dollar;

        var created = Amount.Create(20);

        var parsed = Amount.Parse("20");

        var eeneuro = Amount.Create(2); 

        var tweeentwintig = parsed + eeneuro;
        var veertig = parsed * 2;
        var tien = parsed / 2;

        var currency = Currency.Parse("$");

        var money = Money.Parse("$ 20,00");

        List<Money> all = [ Money.Parse("$ 10,00"), Money.Parse("$ 15,00"), Money.Parse("€ 20,00"), Money.Parse("EUR 30,00") ];

        var mo = all.Sum(Currency.Euro);

        var mo1 = all.Sum(Currency.Dollar);

        var mon = eeneuro + Currency.Euro;

    }
}
