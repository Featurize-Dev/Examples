using Featurize;

namespace WebApp.Features.Cart;

public class CartFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddHttpClient<CartService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001");
        });

        services.AddScoped<CartState>();
        services.AddSingleton<CartService>();
    }
}
