using CommonFeatures.Storage;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Buyer;

public class BuyerFeature : IWebApplicationFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<BuyerAggregate, UserId>(x =>
        {
            x.Provider(EntityFrameworkRepositoryProvider.DefaultName);
            //x.UseContext<Buyer>
        });
    }

    public void Use(WebApplication app)
    {
                
    }
}
