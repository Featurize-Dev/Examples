using GBC.Accounts.Features.Account.ValueObjects;
using GBC.Accounts.Features.Storage;
using GBC.Accounts.Features.Transactions.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace GBC.Accounts.Features.Transactions.Commands;

public static class CreateTransaction
{
    public static IEndpointRouteBuilder MapCreateTransaction(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Route, Handle);
        return builder;
    }

    public static string Route => "";

    public static async Task<Results<Ok<TransactionId>, BadRequest>> Handle(
        [FromServices] AggregateManager<Transaction, TransactionId> manager,
        [FromBody] CreateTransactionRequest request
        )
    {
        var transaction = Transaction.Create(request.from, request.to, request.amount);
        await manager.SaveAsync(transaction);
        return TypedResults.Ok(transaction.Id);
    }
}

public record CreateTransactionRequest(BankAccountNumber from, BankAccountNumber to, Amount amount);
