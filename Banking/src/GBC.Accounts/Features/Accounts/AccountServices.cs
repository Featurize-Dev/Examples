using GBC.Accounts.Features.Accounts.ValueObjects;
using GBC.Accounts.Features.Storage;

namespace GBC.Accounts.Features.Accounts;

public class AccountServices(AggregateManager<Account, BankAccountNumber> manager)
{
    public AggregateManager<Account, BankAccountNumber> Manager { get; } = manager;
}
