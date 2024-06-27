using Common.ValueObjects;

namespace Hypotheken.Domain.Inkomens;

public class HuurInkomsten(Amount jaarInkomen) : Inkomen
{
    public override DateOnly IngangsDatum => throw new NotImplementedException();

    public override DateOnly EindDatum => throw new NotImplementedException();

    public override Amount JaarInkomen => jaarInkomen;
}