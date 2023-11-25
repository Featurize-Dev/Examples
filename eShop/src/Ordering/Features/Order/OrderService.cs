using CommonFeatures.Storage;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderServices(ILogger<OrderServices> logger, AggregateManager<OrderAggregate, OrderId> manager)
{
    public AggregateManager<OrderAggregate, OrderId> Manager { get; } = manager;
    public ILogger<OrderServices> Logger { get; } = logger;
}
