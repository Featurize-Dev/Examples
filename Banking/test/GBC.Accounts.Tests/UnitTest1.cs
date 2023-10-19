using Featurize.ValueObjects;
using GBC.Accounts.Features.Account.ValueObjects;

namespace GBC.Accounts.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var bc = BankAccountNumber.Parse("NL78ABNA0498857158");
        
        Assert.IsNotNull(bc);
    }
}