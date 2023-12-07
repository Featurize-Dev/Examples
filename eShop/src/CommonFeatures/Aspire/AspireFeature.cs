using Featurize;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommonFeatures.Aspire;
public class AspireFeature : IServiceCollectionFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddServiceDiscovery();

        services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();

            http.UseServiceDiscovery();
        });
    }

}
