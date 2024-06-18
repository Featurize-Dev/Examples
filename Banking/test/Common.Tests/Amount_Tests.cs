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

        var parsed = Amount.Parse("€ 20");

        var eeneuro = Amount.Create(2, Currency.Euro); 

        var tweeentwintig = parsed + eeneuro;
        var veertig = parsed * 2;
        var tien = parsed / 2;

    }
}
