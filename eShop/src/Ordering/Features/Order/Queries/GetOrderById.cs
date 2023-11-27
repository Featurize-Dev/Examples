using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Queries;

public static class GetOrderById
{
    public static void MapGetOrderById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("{orderId}", Handle);
    }

    public static async Task<Results<Ok<OrderAggregate>, NotFound>> Handle(
        [FromHeader(Name = "x-requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        OrderId orderId)
    {
        var order = await services.Manager.LoadAsync(orderId);

        if(order is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(order);
    }
}
