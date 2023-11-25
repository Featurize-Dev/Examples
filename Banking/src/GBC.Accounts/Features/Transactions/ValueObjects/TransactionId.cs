using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace GBC.Accounts.Features.Transactions.ValueObjects;

public record struct TransactionId() : IValueObject<TransactionId>
{
    private const string _unknownValue = "?";
    private Guid? _value = Guid.NewGuid();
    public static TransactionId Unknown 
        => new() {  _value = null };

    public static TransactionId Empty 
        => new() { _value = Guid.Empty };

    public override readonly string ToString() 
        => _value?.ToString() ?? _unknownValue;

    public static TransactionId Parse(string s)
        => Parse(s, null);

    public static TransactionId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out TransactionId result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TransactionId result)
    {
        if(string.IsNullOrEmpty(s))
        {
            result = Empty;
            return true;
        }

        if(s == _unknownValue)
        {
            result = Unknown;
            return true;
        }

        if(Guid.TryParse(s, out var id))
        {
            result = new() {  _value = id };
            return true;
        }

        result = Unknown;
        return false;
    }

    public readonly bool IsEmpty()
        => this == Empty;
}
