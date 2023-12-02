using CommonFeatures.Storage;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Ordering.Features.Buyer.ValueObjects;

namespace Ordering.Features.Buyer;

public class BuyerFeature : IWebApplicationFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(IServiceCollection services)
    {
        var con = "Host=nas.home.lab;Database=OrdersDB;Username=postgresUser;Password=postgresPW";

        services.AddDbContext<BuyerContext>(options =>
        {
            options.UseNpgsql(con);
        });
    }

    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<BuyerAggregate, BuyerId>(x =>
        {
            x.Provider(EntityFrameworkRepositoryProvider.DefaultName);
            x.UseContext<BuyerContext>();
        });
    }

    public void Use(WebApplication app)
    {
                
    }
}
