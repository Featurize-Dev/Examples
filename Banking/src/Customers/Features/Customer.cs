using Customers.Features.Entities;
using Customers.Features.ValueObjects;
using Featurize.DomainModel;
using Featurize.ValueObjects;

namespace Customers.Features;

public class Customer : AggregateRoot<CustomerId>
{
    public static Customer Create(string firstname, string surname, Address address, PhoneNumber phone, EmailAddress emailAddress)
    {
        var customer = new Customer();
        customer.RecordEvent(new CustomerManualCreated(customer.Id, firstname, surname, address, phone, emailAddress));
        return customer;
    }

    private Customer() : base(new CustomerId())
    {
    }

    public string Firstname { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;
    public Address Address { get; private set; } = new Address();
    public PhoneNumber Phone { get; private set; } = PhoneNumber.Unknown;
    public EmailAddress EmailAddress { get; private set; } = EmailAddress.Unknown;


    internal void Apply(CustomerManualCreated e)
    {
        Firstname = e.Firstname;
        Surname = e.Surname;
        Address = e.Address;
        Phone = e.Phone;
        EmailAddress = e.EmailAddress;
    }
}

public record CustomerManualCreated(
    CustomerId Id,
    string Firstname,
    string Surname,
    Address Address,
    PhoneNumber Phone,
    EmailAddress EmailAddress
    ) : EventRecord;
