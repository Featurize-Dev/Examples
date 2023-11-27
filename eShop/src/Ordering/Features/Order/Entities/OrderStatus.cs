using System.Linq;

namespace Ordering.Features.Order.Entities;

public record OrderStatus(int Id, string Name)
{
    public static OrderStatus Unknown = new(0, nameof(Unknown).ToLowerInvariant());
    public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());
    public static OrderStatus AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());
    public static OrderStatus StockConfirmed = new(3, nameof(StockConfirmed).ToLowerInvariant());
    public static OrderStatus Paid = new(4, nameof(Paid).ToLowerInvariant());
    public static OrderStatus Shipped = new(5, nameof(Shipped).ToLowerInvariant());
    public static OrderStatus Cancelled = new(6, nameof(Cancelled).ToLowerInvariant());

    public static IEnumerable<OrderStatus> All() =>
        new[] { Submitted, AwaitingValidation, StockConfirmed, Paid, Shipped, Cancelled };

    public static OrderStatus FromName(string name)
    {
        var state = All()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        return state ?? throw new InvalidOperationException();
    }

    public static OrderStatus FromId(int id)
    {
        var state = All() 
            .SingleOrDefault(s => s.Id == id);
        return state ?? throw new InvalidOperationException();
    }
}
