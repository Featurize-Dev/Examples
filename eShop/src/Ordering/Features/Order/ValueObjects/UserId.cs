using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Ordering.Features.Order.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record struct UserId() : IValueObject<UserId>
{
    private const string _unknownValue = "?";
    private Guid? _id = Guid.NewGuid();

    public override readonly string ToString()
        => _id?.ToString() ?? _unknownValue;

    public static UserId Unknown => new() { _id = null };

    public static UserId Empty => new() { _id = Guid.Empty };

    public static UserId Parse(string s)
        => Parse(s, null);

    public static UserId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out UserId result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out UserId result)
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

        if (Guid.TryParse(s, out var id))
        {
            result = new() { _id = id };
            return true;
        }

        result = Unknown;
        return false;
    }

    public readonly bool IsEmpty()
        => this == Empty;
}


