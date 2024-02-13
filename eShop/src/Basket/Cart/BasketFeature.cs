using Featurize.AspNetCore;

namespace BasketApi.Cart;

public class CartFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddGrpc();

        services.AddSingleton<BasketStore>();
    }

    public void Use(WebApplication app)
    {
        app.MapGrpcService<BasketService>();
    }
}
