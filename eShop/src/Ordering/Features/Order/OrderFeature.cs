using CommonFeatures.Storage;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Ordering.Features.Order.Commands;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderFeature : IWebApplicationFeature, IConfigureOptions<RepositoryProviderOptions>
{
    public void Configure(RepositoryProviderOptions options)
    {
        options.AddAggregate<OrderAggregate, OrderId>(o =>
        {
            o.Provider(EntityFrameworkRepositoryProvider.DefaultName);
            o.UseContext<OrderContext>();
        });
    }

    public void Configure(IServiceCollection services)
    {
        var con = "Host=localhost;Database=OrdersDB;Username=postgresUser;Password=postgresPW";

        services.AddDbContext<OrderContext>(options =>
        {
            options.UseNpgsql(con);
        });
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/orders");
            //.RequireAuthorization();

        group.MapCreateOrder();
    }
}
