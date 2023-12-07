using Featurize;
using Featurize.AspNetCore;

namespace WebApp.Features.Catalog;

public class CatalogFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddHttpClient<CatalogService>(client =>
        {
            client.BaseAddress = new Uri("http://catalog-api");
        });
    }
}
