using Featurize.DomainModel;
using GBC.Accounts.Features.Account.ValueObjects;

namespace GBC.Accounts.Features.Account;

public class Account : AggregateRoot<Account, BankAccountNumber>
{
    private Account() : base(new BankAccountNumber())
    {
        RecordEvent(new AccountOpenend());
    }



    internal void Apply(AccountOpenend e)
    {

    }
}


internal record AccountOpenend() : EventRecord;