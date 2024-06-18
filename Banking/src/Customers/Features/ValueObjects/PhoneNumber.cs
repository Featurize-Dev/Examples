using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Customers.Features.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record struct PhoneNumber() : IValueObject<PhoneNumber>
{
    private const string _unknownValue = "?";
    private string _phoneNumber = string.Empty;

    public override readonly string ToString()
        => _phoneNumber;

    public static PhoneNumber Unknown => new() { _phoneNumber  = _unknownValue };

    public static PhoneNumber Empty => new() { _phoneNumber = string.Empty };

    public static PhoneNumber Parse(string s)
        =>Parse(s, null);

    public static PhoneNumber Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out PhoneNumber result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PhoneNumber result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if (s == _unknownValue)
        {
            result = Unknown;
            return true;
        }

        if(PhoneNumberValidator.IsValid(ref s))
        {
            result = new() { _phoneNumber = s };
            return true;
        }

        result = Unknown;
        return false;
    }

    public bool IsEmpty() => this == Empty;
}


public class PhoneNumberValidator
{
    public static bool IsValid(ref string phoneNumber)
    {
        // TODO: Validate PhoneNumber
        if(string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }
        
        return true;
    }
}