﻿
using BasketApi.Grpc;
using WebApp.Features.Cart.Entities;

namespace WebApp.Features.Cart;

public record BasketQuantity(int ProductId, int Quantity);

public class CartService(BasketApi.Grpc.Basket.BasketClient clientClient)
{
    public async Task<IReadOnlyCollection<BasketQuantity>> GetCart()
    {
        var response = await clientClient.GetBasketAsync(new());
        return response.Items.Select(item => new BasketQuantity(item.ProductId, item.Quantity)).ToArray();
    }

    public async Task UpdateCart(IReadOnlyCollection<BasketQuantity> cart)
    {
        var request = new UpdateBasketRequest();

        foreach (var item in cart)
        {
            request.Items.Add(new BasketItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            });
        }

        await clientClient.UpdateBasketAsync(request);
    }

    public async Task DeleteCart()
    {
        // TODO: Implement
    }
}