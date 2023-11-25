using Featurize.DomainModel;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderAggregate : AggregateRoot<OrderAggregate, OrderId>
{
    public OrderAggregate(OrderId id) : base(id)
    {
        RecordEvent(new OrderCreated());
    }
}

public record OrderCreated() : EventRecord;