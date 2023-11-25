using Featurize.DomainModel;
using GBC.Accounts.Features.Account.ValueObjects;
using GBC.Accounts.Features.Transactions.ValueObjects;

namespace GBC.Accounts.Features.Transactions;

public class Transaction : AggregateRoot<Transaction, TransactionId>
{
    public TransactionStatus Status { get; private set; }
    public BankAccountNumber From { get; private set; } 
    public BankAccountNumber To { get; private set; }
    public Amount Amount { get; private set; }

    public bool Withdrawn { get; private set; }
    public bool Deposited { get; private set; }

    private Transaction() : base(new TransactionId())
    {
        RecordEvent(new TransactionInitialized());
    }

    public static Transaction Create(BankAccountNumber from, BankAccountNumber to, Amount amount)
    {
        if(amount == Amount.Zero || amount < Amount.Zero)
        {
            throw new ArgumentException($"Amount can not be {amount}.");
        }

        var transaction = new Transaction();
        transaction.RecordEvent(new TransactionCreated(from, to, amount));
        return transaction;
    }

    public Transaction Finished()
    {
        if(Status != TransactionStatus.CREATED)
            throw new InvalidOperationException($"Transaction in state: {Status} can not be finished!");

        RecordEvent(new TransactionFinished());
        return this;
    }

    public Transaction Failed()
    {
        RecordEvent(new TransactionFailed());
        return this;
    }

    public Transaction Cancel()
    {
        if(Status != TransactionStatus.CREATED)
            throw new InvalidOperationException($"Transaction in state: {Status} can not be Canceled!");

        
        return this;
    }

    public Transaction Deposit()
    {
        if(Status == TransactionStatus.FAILED)
        {
            RecordEvent(new DepositRolledBack(From, To, Amount));
        } 
        else
        {
            RecordEvent(new Deposited());
        }

        return this;
    }

    public Transaction WithDraw()
    {
        return this;
    }

    internal void Apply(TransactionInitialized e)
    {
        Status = TransactionStatus.INITIALIZED;
        From = BankAccountNumber.Unknown;
        To = BankAccountNumber.Unknown;
        Amount = Amount.Unknown;
    }

    internal void Apply(TransactionCreated e)
    {
        Status = TransactionStatus.CREATED;
        From = e.From;
        To = e.To;
        Amount = e.Amount;
    }

    internal void Apply(TransactionFinished e)
    {
        Status = TransactionStatus.FINISHED;
    }

    internal void Apply(TransactionFailed e)
    {
        Status = TransactionStatus.FAILED;
    }
}

public enum TransactionStatus
{
    INITIALIZED = 0,
    CREATED = 5,
    FAILED = 9,
    FINISHED = 10,
    
}


public record TransactionInitialized() : EventRecord;
public record TransactionCreated(BankAccountNumber From, BankAccountNumber To, Amount Amount) : EventRecord;
public record TransactionFinished() : EventRecord;
public record TransactionFailed() : EventRecord;
public record DepositRolledBack(BankAccountNumber From, BankAccountNumber To, Amount Amount) : EventRecord;
public record Deposited() : EventRecord;