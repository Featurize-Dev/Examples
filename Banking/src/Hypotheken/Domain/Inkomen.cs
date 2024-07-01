using Common.ValueObjects;
using Hypotheken.Domain.Inkomens;

namespace Hypotheken.Domain;

public abstract class Inkomen
{
    public abstract DateOnly IngangsDatum {get; }

    public abstract DateOnly EindDatum { get; }

    public abstract Amount JaarInkomen { get; }
}

public static class InkomenFactory
{
    public static Inkomen Verzamel(params Inkomen[] typeInkomen)
        => VerzamelInkomen.Create(typeInkomen);

    public static Inkomen VastContract(Amount perjaar)
        => new Loondienst(new VastContract(), perjaar);
}