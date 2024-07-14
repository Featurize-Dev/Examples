using Common.ValueObjects;
using Hypotheken.Domain.HypotheekGevers;

namespace Hypotheken.Domain;

public abstract class Hypotheekgever
{
    public static Hypotheekgever Empty()
        => new Samengesteld();

    public static Hypotheekgever NatuurlijkPersoon() 
        => new NatuurlijkPersoon();

    public static Hypotheekgever Rechtspersoon()
        => new Rechtspersoon();

    public abstract Amount JaarInkomen { get; }
}
