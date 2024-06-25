using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Common.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public partial record struct Amount : IValueObject<Amount>
{
    private decimal? _value = 0;
    public readonly Currency Currency { get; }
    
    private Amount(decimal? value, Currency currency)
    {
        _value = value;
        Currency = currency;
    }

    public static Amount Unknown => new(null, Currency.Default);

    public static Amount Empty => new(0, Currency.Default);
    public static Amount Zero => new(0, Currency.Default);

    public static Amount One => new(1, Currency.Default);

    public static Amount Create(decimal value)
        => new(value, Currency.Default);
    
    public static Amount Create(decimal value, Currency currency)
        => new(value, currency);

    public static Amount Create(int value)
        => Create((decimal)value, Currency.Default);

    public static Amount Create(double value)
        => Create((decimal)value);

    public static Amount Create(int value, Currency currency)
        => Create((decimal)value, currency);

    public static Amount Create(double value, Currency currency)
        => Create((decimal)value, currency);

    public override string ToString()
        => CurrencyFormatter.FormatCurrency(Currency, _value ?? 0, 2);

    public static Amount Parse(string s)
        => Parse(s, null);

    public static Amount Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out Amount result) ? result : Unknown;

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Amount result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Amount result)
    {
        result = Empty;

        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        // Split the input string to separate the currency symbol and value
        string[] parts = s.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 2)
        {
            // Attempt to parse the value part
            if (decimal.TryParse(parts[1], NumberStyles.Any, provider, out decimal value))
            {
                // Determine the currency based on the symbol
                Currency currency = parts[0] switch
                {
                    "€" => Currency.Euro,
                    "$" => Currency.Dollar,
                    _ => Currency.Default // Default to the default currency if the symbol is not recognized
                };

                result = new Amount(value, currency);
                return true;
            }
        }

        result = Unknown;

        return false;
    }

    public bool IsEmpty()
        => this == Empty;

    public static implicit operator Amount(decimal val) => Create(val);
    public static explicit operator decimal(Amount val) => val._value ?? 0;
    public static explicit operator double(Amount val) => (double)(val._value ?? 0);
}

public partial record struct Amount :
    IIncrementOperators<Amount>,
    IDecrementOperators<Amount>,
    IUnaryPlusOperators<Amount, Amount>,
    IUnaryNegationOperators<Amount, Amount>,
    IAdditionOperators<Amount, Amount, Amount>,
    ISubtractionOperators<Amount, Amount, Amount>,
    IMultiplyOperators<Amount, decimal, Amount>,
    IMultiplyOperators<Amount, int, Amount>,
    IMultiplyOperators<Amount, double, Amount>,
    IMultiplyOperators<Amount, Percentage, Amount>,
    IDivisionOperators<Amount, decimal, Amount>,
    IDivisionOperators<Amount, double, Amount>,
    IDivisionOperators<Amount, int, Amount>
{
    public static Amount operator +(Amount value)
        => new(+value._value, value.Currency);
    public static Amount operator -(Amount value)
       => new(-value._value, value.Currency);

    public static Amount operator +(Amount left, Amount right)
    {
        if(left.Currency == right.Currency)
        {
            return new Amount(left._value + right._value, left.Currency);
        }

        throw new InvalidOperationException("Currencies are not equal.");
    }

    public static Amount operator -(Amount left, Amount right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Currencies are not equal.");
        }

        return new Amount(left._value - right._value, left.Currency);
    }

    public static Amount operator ++(Amount value)
        => new(value._value++, value.Currency);

    public static Amount operator --(Amount value)
        => new(value._value--, value.Currency);

    public static Amount operator *(Amount left, decimal right)
        => new(left._value * right, left.Currency);

    public static Amount operator /(Amount left, decimal right)
        => new (left._value / right, left.Currency);

    public static Amount operator *(Amount left, Percentage right)
        => new (left._value * (decimal)right, left.Currency);

    public static bool operator >(Amount left, Amount right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Currencies are not equal.");
        }

        return left._value > right._value;
    }

    public static bool operator >=(Amount left, Amount right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Currencies are not equal.");
        }

        return right._value >= left._value;
    }

    public static bool operator <(Amount left, Amount right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Currencies are not equal.");
        }

        return left._value < right._value;
    }

    public static bool operator <=(Amount left, Amount right)
    {
        if (left.Currency != right.Currency)
        {
            throw new InvalidOperationException("Currencies are not equal.");
        }

        return left._value <= right._value;
    }

    public static Amount operator %(Amount left, decimal right)
        => new(left._value % right, left.Currency);

    public static Amount operator *(Amount left, double right)
        => new(left._value * (decimal)right, left.Currency);

    public static Amount operator /(Amount left, double right)
        => new(left._value / (decimal)right, left.Currency);

    public static Amount operator *(Amount left, int right)
        => new(left._value * right, left.Currency);

    public static Amount operator /(Amount left, int right)
        => new(left._value / right, left.Currency);
}

public record Currency(string Symbol, string Code, string Unit)
{
    public static Currency Default { get; set; } = Currency.Euro;
    public static Currency Euro => new("€", "EUR", "Euro");
    public static Currency Dollar => new("$", "USD", "United States Dollar");
}

public class CurrencyFormatter
{
    public static string FormatCurrency(Currency currency, decimal amount, int decPlaces)
    {
        NumberFormatInfo localFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        localFormat.CurrencySymbol = currency.Symbol;
        localFormat.CurrencyDecimalDigits = decPlaces;
        return amount.ToString("c", localFormat);
    }
}