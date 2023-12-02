using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Commands;

public static class CancelOrder
{
    public static void MapCancelOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/cancel", Handle);
    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> Handle(
        [FromHeader(Name = "x-requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        CancelOrderRequest request)
    {
        var order = await services.Manager.LoadAsync(request.OrderNumber);

        if(order is null)
        {
            return TypedResults.NotFound();
        }

        order.Cancel();

        await services.Manager.SaveAsync(order);

        return TypedResults.Ok();
    }
}

public record CancelOrderRequest(OrderId OrderNumber);