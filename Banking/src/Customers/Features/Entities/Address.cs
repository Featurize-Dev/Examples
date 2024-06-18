using Customers.Features.ValueObjects;
using Featurize.ValueObjects;

namespace Customers.Features.Entities;

public record struct Address()
{
    public AddressTypes AddresType { get; init; }
    public string Street { get; init; }
    public PostalCode PostalCode { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public Country Country { get; init; }
}

public enum AddressTypes
{
    Mailing,
    Shipping,
    Billing,
    Home
}