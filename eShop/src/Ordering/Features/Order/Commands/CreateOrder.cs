using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Commands;

public static class CreateOrder
{
    public static void MapCreateOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", Handle);
    }

    public static async Task<Results<Ok, BadRequest<string>>> Handle(
        [FromHeader(Name = "requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        CreateOrderRequest request
        )
    {
        var order = new OrderAggregate(new OrderId());

        await services.Manager.SaveAsync(order);

        return TypedResults.Ok();
    }
}

public class CreateOrderRequest();