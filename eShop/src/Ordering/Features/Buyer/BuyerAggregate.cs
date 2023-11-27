using Featurize.DomainModel;
using Ordering.Features.Buyer.Entities;
using Ordering.Features.Buyer.ValueObjects;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Buyer;

public class BuyerAggregate : AggregateRoot<BuyerAggregate, BuyerId>
{
    private readonly List<PaymentMethod> _paymentMethods = [];

    public string Name { get; private set; } = string.Empty;

    public PaymentMethod[] PaymentMethods => _paymentMethods.ToArray();

    private BuyerAggregate(BuyerId id) : base(id)
    {
    }

    public static BuyerAggregate Create(UserId id, string Username)
    {
        var buyer = new BuyerAggregate(new BuyerId());
        buyer.RecordEvent(new BuyerCreated(id, Username));
        return buyer;
    }
}

public record BuyerCreated(UserId userId, string Username) : EventRecord;