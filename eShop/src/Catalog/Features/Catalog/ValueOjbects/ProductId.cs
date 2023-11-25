using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Catalog.Features.Catalog.ValueOjbects;

public record ProductId : IValueObject<ProductId>
{
    public static ProductId Unknown => throw new NotImplementedException();

    public static ProductId Empty => throw new NotImplementedException();

    public static ProductId Parse(string s)
    {
        throw new NotImplementedException();
    }

    public static ProductId Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out ProductId result)
    {
        throw new NotImplementedException();
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ProductId result)
    {
        throw new NotImplementedException();
    }

    public bool IsEmpty()
    {
        throw new NotImplementedException();
    }
}
