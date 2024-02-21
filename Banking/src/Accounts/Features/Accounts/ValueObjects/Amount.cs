using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GBC.Accounts.Features.Accounts.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record struct Amount() : IValueObject<Amount>
{
    private const string _unknowValue = "?";
    private decimal? _value = 0;

    public static Amount Unknown
        => new() { _value = null };

    public static Amount Empty
        => new() { _value = 0 };

    public static Amount Zero
        => Empty;

    public static Amount Max
        => new() { _value = decimal.MaxValue };

    public static Amount Min
        => new() { _value = decimal.MinValue };

    public override readonly string ToString()
        => _value?.ToString() ?? _unknowValue;

    public static Amount Parse(string s)
        => Parse(s, null);

    public static Amount Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Amount result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Amount result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = Empty;
            return false;
        }

        if (s == _unknowValue)
        {
            result = Unknown;
            return true;
        }

        if (decimal.TryParse(s, out var value))
        {
            result = new() { _value = value };
            return true;
        }

        result = Unknown;
        return false;
    }

    public readonly bool IsEmpty()
        => this == Empty;

    public static Amount operator +(Amount a, Amount b)
        => new() { _value = a._value + b._value };

    public static Amount operator -(Amount a, Amount b)
        => new() { _value = -a._value - b._value };

    public static Amount operator *(Amount a, Amount b)
        => new() { _value = a._value * b._value };

    public static Amount operator /(Amount a, Amount b)
        => new() { _value = a._value / b._value };

    public static bool operator <(Amount a, Amount b)
        => a._value < b._value;
    public static bool operator >(Amount a, Amount b)
        => !(a < b);
}

