using Common.ValueObjects;
using FluentAssertions;

namespace Common.Tests;

public class BSN_Tests
{
    [Fact]
    public void Test1()
    {
        var bsn = BSN.Create(138328559);

        var bsn1 = BSN.Create(138328556);

    }

    [Fact]
    public void Percentages()
    {
        var percentage = Percentage.Create(1);
        var percentage1 = Percentage.Create(100);
        var pMile = Percentage.Create(0.1m);

        var p = Percentage.Parse("1%");
        var p100 = Percentage.Parse("100%");
        var pmile = Percentage.Parse("1‰");

        percentage.Should().Be(p);
        percentage1.Should().Be(p100);
        pmile.Should().Be(pMile);

    }
}