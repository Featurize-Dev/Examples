using Common.Storage;
using GBC.Accounts.Features.Accounts.ValueObjects;

namespace GBC.Accounts.Features.Accounts;

public class AccountServices(AggregateManager<Account, BankAccountNumber> manager)
{
    public AggregateManager<Account, BankAccountNumber> Manager { get; } = manager;
}
