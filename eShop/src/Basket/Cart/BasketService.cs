using BasketApi.Cart.Entities;
using BasketApi.Grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace BasketApi.Cart;

public class BasketService(BasketStore store, ILogger<BasketService> logger) : Basket.BasketBase
{
    [AllowAnonymous]
    public override async Task<CustomerBasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
    {
        //var userId = context.GetHttpContext().User.FindFirst("sub")?.Value;

        //if (string.IsNullOrEmpty(userId))
        //{
        //    return new();
        //}

        var userId = "123";

        var data = await store.GetBasketAsync(userId);

        if(data is null)
        {
            return new();
        }

        return MapToResponse(data);
    }

    public override async Task<CustomerBasketResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
    {
        //var userId = context.GetHttpContext().User.FindFirst("sub")?.Value;

        //if (string.IsNullOrEmpty(userId))
        //{
        //    return new();
        //}
        var userId = "123";
        var basket = MapToEntity(request, userId);
        var data = await store.UpdateBasketAsync(basket);

        if (data is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Basket with buyer id {userId} does not exist"));
        }

        return MapToResponse(data);
    }

    public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
    {
        var userId = context.GetHttpContext().User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "The caller is not authenticated."));
        }

        await store.DeleteBasketAsync(userId);

        return new();
    }

    private CustomerBasket MapToEntity(UpdateBasketRequest request, string userId)
    {
        var basket = new CustomerBasket
        {
            UserId = userId,
            Items = request.Items.Select(x => new Entities.BasketItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };

        return basket;
    }

    private CustomerBasketResponse MapToResponse(CustomerBasket basket)
    {
        var response = new CustomerBasketResponse();

        foreach(var item in basket.Items)
        {
            response.Items.Add(new Grpc.BasketItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }   

        return response;
    }
}
