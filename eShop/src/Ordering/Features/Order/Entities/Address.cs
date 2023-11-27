namespace Ordering.Features.Order.Entities;

public record Address(string street, string city, string state, string country, string zipcode)
{
    public static Address Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
};
