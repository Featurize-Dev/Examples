using CommonFeatures.Storage;
using Featurize;
using Featurize.AspNetCore;
using Featurize.Repositories;
using Featurize.Repositories.EntityFramework;
using Kafka;
using Microsoft.EntityFrameworkCore;
using Ordering.Features.Order.Commands;
using Ordering.Features.Order.Queries;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderFeature : IWebApplicationFeature,
    IConfigureOptions<RepositoryProviderOptions>,
    IConfigureOptions<KafkaOptions>
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

    public void Configure(KafkaOptions options)
    {
        options.AddProducer<OrderId, PersistendEvent<OrderId>>(options =>
        {
            options.Topic = "order.events";
        });

        options.AddProducer<OrderId, OrderAggregate>(options =>
        {
            options.Topic = "order.snapshot";
        });
    }

    public void Use(WebApplication app)
    {
        var group = app.MapGroup("/api/v1/orders")
            .WithTags("Ordering");
            //.RequireAuthorization();

        group.MapCreateOrder();
        group.MapCreateDraftOrder();
        group.MapShipOrder();
        group.MapCancelOrder();

        group.MapGetOrderById();
    }
}
