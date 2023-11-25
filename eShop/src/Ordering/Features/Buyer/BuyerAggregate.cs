using Featurize.DomainModel;
using Ordering.Features.Buyer.ValueObjects;

namespace Ordering.Features.Buyer;

public class BuyerAggregate : AggregateRoot<BuyerAggregate, BuyerId>
{
    public BuyerAggregate(BuyerId id) : base(id)
    {
        RecordEvent(new BuyerCreated());
    }


}

public record BuyerCreated() : EventRecord;