using BasketApi.Grpc;
using Featurize;

namespace WebApp.Features.Cart;

public class CartFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddGrpcClient<Basket.BasketClient>(client =>
        {
            client.Address = new Uri("http://basket-api");
        });

        services.AddScoped<CartState>();
        services.AddSingleton<CartService>();
    }
}
