
using WebApp.Features.Cart.Entities;

namespace WebApp.Features.Cart;

public class CartService(HttpClient httpClient)
{
    public async Task DeleteCart()
    {
        // TODO: Implement
    }

    public async Task<IReadOnlyCollection<CartItem>> GetCart()
    {
        return Array.Empty<CartItem>();
    }
}