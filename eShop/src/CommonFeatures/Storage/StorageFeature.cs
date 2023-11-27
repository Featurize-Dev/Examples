using Featurize;
using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace CommonFeatures.Storage;
public class StorageFeature : IServiceCollectionFeature, IConfigureOptions<RepositoryProviderOptions>, IConfigureOptions<KafkaOptions>
{
    public void Configure(IServiceCollection services)
    {
        services.AddScoped(typeof(AggregateManager<,>));
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddEntityFramework(options => { });
    }

    public void Configure(KafkaOptions options)
    {
        
    }
}
