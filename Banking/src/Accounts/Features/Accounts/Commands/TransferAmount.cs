using Common.Logging;
using GBC.Accounts.Features.Accounts.ValueObjects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GBC.Accounts.Features.Accounts.Commands;

public record TransferAmountRequest(BankAccountNumber From, BankAccountNumber To, Amount Amount);

public static class TransferAmount
{
    public static void MapTransfrer(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/transfer", Handle);
    }

    public static async Task<Results<Ok, BadRequest>> Handle(
        [FromHeader(Name ="x-requestid")] RequestId requestId,
        [AsParameters] AccountServices services,
        [FromBody] TransferAmountRequest request

        )
    {
        var from = await services.Manager.LoadAsync(request.From);
        if(from is null)
        {
            return TypedResults.BadRequest();
        }

        var resrved = from.Reserve(request.Amount);
        if (!resrved)
        {
            return TypedResults.BadRequest();
        }

        await services.Manager.SaveAsync(from);

        var to = await services.Manager.LoadAsync(request.To);
        if(to is null)
        {
            return TypedResults.BadRequest();
        }

        to.Deposit(request.Amount, request.From);
        await services.Manager.SaveAsync(to);

        from.WithDraw(request.Amount);
        await services.Manager.SaveAsync(from);

        return TypedResults.Ok();
    }
}
