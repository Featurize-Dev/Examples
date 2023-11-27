using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Features.Order.ValueObjects;
using System.Runtime.CompilerServices;

namespace Ordering.Features.Order.Commands;

public static class ShipOrder
{
    public static void MapShipOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPut("/ship", Handle);
    }

    public async static Task<Results<Ok, NotFound>> Handle(
        [FromHeader(Name = "x-requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        ShipOrderRequest request)
    {
        var order = await services.Manager.LoadAsync(request.OrderNumber);

        if(order is null)
        {
            return TypedResults.NotFound();
        }

        order.Ship();

        await services.Manager.SaveAsync(order);
        await services.Manager.PublishAsync("order", order);

        return TypedResults.Ok();
    }
}

public record ShipOrderRequest(OrderId OrderNumber);