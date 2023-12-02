using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.Features.Order.Entities;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order.Commands;

public static class CreateOrder
{
    public static void MapCreateOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", Handle);
    }

    public static async Task<Results<Ok, BadRequest<string>>> Handle(
        [FromHeader(Name = "x-requestid")] RequestId requestId,
        [AsParameters] OrderServices services,
        CreateOrderRequest request)
    {
        var order = OrderAggregate.Create(request.CustomerInfo, request.Address, request.Items);

        await services.Manager.SaveAsync(order);

        return TypedResults.Ok();
    }
}

public record CreateOrderRequest(UserInfo CustomerInfo, Address Address, List<OrderItem> Items);