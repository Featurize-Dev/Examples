using System.Linq;

namespace Ordering.Features.Order.Entities;

public class OrderStatus(int id, string name)
{

    public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());

    public int Id { get; } = id;
    public string Name { get; } = name;

    public static IEnumerable<OrderStatus> List() =>
        new[] { Submitted };

    public static OrderStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        return state ?? throw new InvalidOperationException();
    }

    public static OrderStatus FromId(int id)
    {
        var state = List() 
            .SingleOrDefault(s => s.Id == id);
        return state ?? throw new InvalidOperationException();
    }
}
