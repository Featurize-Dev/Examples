using Featurize.ValueObjects.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Featurize.Todo.Features.Todo.ValueObjects;

public record struct TodoId() : IValueObject<TodoId>
{
    private const string UnknownValue = "?";
    private Guid? _id = Guid.NewGuid();
    public static TodoId Unknown => new() { _id = null };

    public static TodoId Empty => new() { _id = Guid.Empty };

    public static TodoId Parse(string s)
        => Parse(s, null);

    public static TodoId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out TodoId result)
        => TryParse(s, null, out result);

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TodoId result)
    {
        if(string.IsNullOrEmpty(s)) 
        {
            result = Empty;        
            return true;   
        }

        if(s == UnknownValue)
        {
            result = Unknown;
            return true;
        }

        if(Guid.TryParse(s, out var id))
        {
            result = new() { _id = id };
            return true;
        }

        result = Unknown;
        return false;
    }

    public bool IsEmpty()
        => this == Empty;
}
