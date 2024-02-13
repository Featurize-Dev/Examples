using BasketApi.Cart.Entities;
using BasketApi.Grpc;

namespace BasketApi.Cart;

public class BasketStore
{
    private List<CustomerBasket> _customerBaskets = new();

    public async Task<CustomerBasket?> GetBasketAsync(string userId)
    {
        return _customerBaskets.Where(x => x.UserId == userId).FirstOrDefault();
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
    {
        //if (!_customerBaskets.Any(x => x.UserId == basket.UserId))
        //{
        //   return null; 
        //}

        _customerBaskets.RemoveAll(x => x.UserId == basket.UserId);
        _customerBaskets.Add(basket);

        return basket;
    }

    public async Task DeleteBasketAsync(string userId)
    {
        _customerBaskets.RemoveAll(x => x.UserId == userId);
    }
}
