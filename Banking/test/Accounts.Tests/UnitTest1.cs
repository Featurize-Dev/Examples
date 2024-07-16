using Featurize.ValueObjects;
using GBC.Accounts.Features.Accounts.ValueObjects;

namespace GBC.Accounts.Tests;

public class Tests
{
    public void Setup()
    {
    }

    public void Test1()
    {
        var bc = BankAccountNumber.Parse("NL78ABNA0498857158");
        
        //Assert.That(bc != BankAccountNumber.Unknown);
    }
}


public class IBanParser
{
    public Dictionary<string, string> GetParts(string iban)
    {
        return new Dictionary<string, string>();
    }
}