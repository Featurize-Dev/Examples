using Featurize.DomainModel;
using GBC.Accounts.Features.Accounts.ValueObjects;

namespace GBC.Accounts.Features.Accounts;


public class Account : AggregateRoot<Account, BankAccountNumber>
{
    public Amount Balance { get; private set; }
    public Amount Reserved { get; private set; }

    private Account() : base(new BankAccountNumber())
    {
        RecordEvent(new AccountOpenend(Amount.Zero));
    }

    public bool Reserve(Amount amount)
    {
        if(Balance - amount < Amount.Zero)
        {
            return false;
        }

        RecordEvent(new ReserveAmount(amount));

        return true;
    }

    public void Deposit(Amount amount, BankAccountNumber from)
    {
        RecordEvent(new DepositAmount(from, amount));
    }

    public void WithDraw(Amount amount)
    {
        RecordEvent(new WitdrawAmount(amount));
    }

    internal void Apply(AccountOpenend e)
    {
        Balance = e.Balance;
        Reserved = Amount.Zero;
    }

    internal void Apply(ReserveAmount e)
    {
        Balance -= e.Value;
        Reserved += e.Value;
    }
}


internal record AccountOpenend(Amount Balance) : EventRecord;
internal record ReserveAmount(Amount Value) : EventRecord;
internal record DepositAmount(BankAccountNumber From, Amount Value) : EventRecord;
internal record WitdrawAmount(Amount Value) : EventRecord;