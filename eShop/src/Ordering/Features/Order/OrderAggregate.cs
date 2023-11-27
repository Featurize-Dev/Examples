using Featurize.DomainModel;
using Ordering.Features.Buyer.ValueObjects;
using Ordering.Features.Order.Entities;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderAggregate : AggregateRoot<OrderAggregate, OrderId>
{
    private readonly List<OrderItem> _orderItems = [];

    private OrderAggregate(OrderId id) : base(id)
    {
    }

    public bool IsDraft { get; private set; } = false;
    public BuyerId BuyerId { get; private set; } = BuyerId.Empty;
    public Address Address { get; private set; } = Address.Empty;
    public OrderItem[] OrderItems => _orderItems.ToArray();
    
    public static OrderAggregate Create(UserInfo userInfo, Address address, List<OrderItem> orderItems)
    {
        var order = new OrderAggregate(new OrderId());
        order.RecordEvent(new OrderCreated(userInfo, address, orderItems, false));
        return order;
    }

    public static OrderAggregate CreateDraft()
    {
        var order = new OrderAggregate(new OrderId());
        order.RecordEvent(new OrderCreated(UserInfo.Empty, Address.Empty, [], true));
        return order;
    }

    public void AddOrderItem(ProductId productId, string productName, decimal price, decimal discount, string pictureUrl, int units = 1)
    {
        var existingItem = _orderItems.Where(x => x.ProductId == productId)
            .SingleOrDefault();

        if (existingItem is not null)
        {
            var dis = (discount > existingItem.Discount) ? discount : existingItem.Discount;

            if(dis != existingItem.Discount && units != existingItem.Units)
            {
                RecordEvent(new OrderItemChanged(productId, dis, units));
            }
        } 
        else
        {
            RecordEvent(new OrderItemAdded(productId, productName, price, discount, pictureUrl, units));
        }
    }

    internal void Apply(OrderCreated e)
    {
        Address = e.Address;  
        _orderItems.AddRange(e.OrderItems);
        IsDraft = e.Draft;
    }

    internal void Apply(OrderItemAdded e)
    {
        _orderItems.Add(new OrderItem(e.ProductId, e.ProductName, e.UnitPrice, e.Discount, e.PictureUrl, e.Units));
    }

    internal void Apply(OrderItemChanged e)
    {
        var exiting = _orderItems.FindIndex(x => x.ProductId == e.ProductId);
        var newItem = _orderItems[exiting] with { Discount = e.Discount, Units = e.Units };
        _orderItems[exiting] = newItem;
    }
}

public record OrderCreated(UserInfo UserInfo, Address Address, List<OrderItem> OrderItems, bool Draft) : EventRecord;
public record OrderItemAdded(ProductId ProductId, string ProductName, decimal UnitPrice, decimal Discount, string PictureUrl, int Units) : EventRecord;
public record OrderItemChanged(ProductId ProductId, decimal Discount, int Units) : EventRecord;