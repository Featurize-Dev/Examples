﻿using Featurize.ValueObjects.Converter;
using Featurize.ValueObjects.Interfaces;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Featurize.Todo.Features.Todos.ValueObjects;

[JsonConverter(typeof(ValueObjectJsonConverter))]
[TypeConverter(typeof(ValueObjectTypeConverter))]
[DebuggerDisplay("{ToString()}")]
public record struct TaskId() : IValueObject<TaskId>
{
    private const string _unknownValue = "?";
    private Guid? _id = Guid.NewGuid();

    public override readonly string ToString()
        => _id?.ToString() ?? _unknownValue;

    public static TaskId Unknown => new() { _id = null };

    public static TaskId Empty => new() { _id = Guid.Empty };

    public static TaskId Parse(string s)
        => Parse(s, null);

    public static TaskId Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider, out var result) ? result : throw new FormatException();

    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out TaskId result)
        => TryParse(s, null, out result);
    

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TaskId result)
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
