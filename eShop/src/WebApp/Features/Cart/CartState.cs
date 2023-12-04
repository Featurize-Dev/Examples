using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Features.Cart.Entities;

namespace WebApp.Features.Cart;

public class CartState(
    CartService cartService,
    CatalogService catalogService,
    AuthenticationStateProvider authenticationStateProvider)
{
    private Task<IReadOnlyCollection<CartItem>>? _cachedBasket;
    private readonly HashSet<CartStateChangedSubscription> _changeSubscriptions = [];

    public Task DeleteCart()
        => cartService.DeleteCart();

    public async Task<IReadOnlyCollection<CartItem>> GetCartItems()
        => await FetchCartItemsAsync();

    public IDisposable NotifyOnChange(EventCallback callback)
    {
        var subscription = new CartStateChangedSubscription(this, callback);
        _changeSubscriptions.Add(subscription);
        return subscription;
    }


    private Task<IReadOnlyCollection<CartItem>> FetchCartItemsAsync()
    {
        return _cachedBasket ??= FetchCore();
        
        async Task<IReadOnlyCollection<CartItem>> FetchCore()
        {
            var quantities = await cartService.GetCart();

            if(quantities.Count == 0)
            {
                return Array.Empty<CartItem>();
            }

            var cartItems = await Task.WhenAll(quantities.Select(async item =>
            {
                var catalogItem = await catalogService.GetCatalogItem(item.CatalogItemId);
                return new CartItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.CatalogItemId,                                                                 
                    Quantity = item.Quantity,
                    ProductName = catalogItem?.Name ?? string.Empty,
                    PictureUrl = catalogItem?.PictureUri ?? string.Empty,
                    UnitPrice = catalogItem?.Price ?? 0,
                };
            }));
            return cartItems;
        }
    }

    private Task NotifyChangeSubscribersAsync()
        => Task.WhenAll(_changeSubscriptions.Select(s => s.NotifyAsync()));

    private class CartStateChangedSubscription(CartState Owner, EventCallback Callback) : IDisposable
    {
        public Task NotifyAsync() => Callback.InvokeAsync();
        public void Dispose() => Owner._changeSubscriptions.Remove(this);
    }
}
