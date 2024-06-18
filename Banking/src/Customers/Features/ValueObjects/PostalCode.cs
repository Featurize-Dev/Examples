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
public record struct PostalCode() : IValueObject<PostalCode>
{
    private const string _unknownValue = "?";
    private string _postalCode = string.Empty;

    public override readonly string ToString()
       => _postalCode;

    public static PostalCode Unknown => new() { _postalCode = _unknownValue };

    public static PostalCode Empty => new() { _postalCode = string.Empty };

    public static PostalCode Parse(string s) => Parse(s, null);

    public static PostalCode Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out PostalCode result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PostalCode result)
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

        if (PostalCodeValidator.IsValid(ref s))
        {
            result = new() { _postalCode = s };
            return true;
        }

        result = Unknown;
        return false;
    }
    public bool IsEmpty() => this == Empty;
}

public static class PostalCodeValidator
{
    public static bool IsValid(ref string postalCode)
    {
        if(string.IsNullOrWhiteSpace(postalCode))
        {
            return false;
        }

        return true;
    }
}
