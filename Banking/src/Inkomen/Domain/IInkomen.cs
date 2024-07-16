using Common.ValueObjects;

namespace Inkomen.Domain;

public interface IInkomen
{
    public Guid Id { get; }
    public string Type { get; }
}
