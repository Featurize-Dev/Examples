using Featurize.DomainModel;
using Ordering.Features.Buyer.ValueObjects;
using Ordering.Features.Order.Entities;
using Ordering.Features.Order.ValueObjects;

namespace Ordering.Features.Order;

public class OrderAggregate : AggregateRoot<OrderAggregate, OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    private OrderStatus _status = OrderStatus.Unknown;

    private OrderAggregate(OrderId id) : base(id)
    {
    }

    public bool IsDraft { get; private set; } = false;
    public string Status => _status.Name;
    public BuyerId BuyerId { get; private set; } = BuyerId.Empty;
    public Address Address { get; private set; } = Address.Empty;
    public OrderItem[] OrderItems => _orderItems.ToArray();

    public decimal Total => _orderItems.Sum(o => o.Units * o.UnitPrice);
    
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

    public void Ship()
    {
        if(_status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Order is not paid.");
        }

        RecordEvent(new OrderShipped());
    }

    public void Cancel()
    {
        if (_status == OrderStatus.Paid ||
           _status == OrderStatus.Shipped)
        {
            throw new InvalidOperationException("Order can not be cancelled.");
        }

        RecordEvent(new OrderCancelled());
    }

    internal void Apply(OrderCreated e)
    {
        Address = e.Address;  
        _orderItems.AddRange(e.OrderItems);
        _status = OrderStatus.Submitted;
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

    internal void Apply(OrderShipped e)
    {
        _status = OrderStatus.Shipped;
    }

    internal void Apply(OrderCancelled e)
    {
        _status = OrderStatus.Cancelled;
    }
}

public record OrderCreated(UserInfo UserInfo, Address Address, List<OrderItem> OrderItems, bool Draft) : EventRecord;
public record OrderItemAdded(ProductId ProductId, string ProductName, decimal UnitPrice, decimal Discount, string PictureUrl, int Units) : EventRecord;
public record OrderItemChanged(ProductId ProductId, decimal Discount, int Units) : EventRecord;
public record OrderShipped() : EventRecord;
public record OrderCancelled() : EventRecord;