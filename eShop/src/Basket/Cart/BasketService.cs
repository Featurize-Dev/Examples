using BasketApi.Grpc;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace BasketApi.Cart;

public class BasketService(ILogger<BasketService> logger) : Basket.BasketBase
{
     [AllowAnonymous]
    public override async Task<CustomerBasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
    {
        logger.LogInformation("GetBasket called");

        var basket = new CustomerBasketResponse();

        basket.Items.Add(new BasketItem()
        {
            ProductId = 1,
            Quantity = 2,
        });

        return basket;
    }
}
