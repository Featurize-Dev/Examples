using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Commands;

public static class CreateDraftOrder
{
    public static void MapCreateDraftOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Draft", Handle);
    }

    public static async Task<Ok> Handle(
        [FromHeader(Name = "x-requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        CreateDraftOrderRequest request)
    {
        var order = OrderAggregate.CreateDraft();

        await services.Manager.SaveAsync(order);

        return TypedResults.Ok();
    }
}


public record CreateDraftOrderRequest();