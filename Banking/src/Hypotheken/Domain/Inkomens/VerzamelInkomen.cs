using Common.ValueObjects;

namespace Hypotheken.Domain.Inkomens;

public class VerzamelInkomen : Inkomen
{
    private readonly IEnumerable<Inkomen> _inkomens;
    private VerzamelInkomen(IEnumerable<Inkomen> inkomens)
    {
        _inkomens = inkomens;
    }

    public override DateOnly IngangsDatum => 
        _inkomens.Min(x => x.IngangsDatum);

    public override DateOnly EindDatum => 
        _inkomens.Max(x => x.EindDatum);

    public override Amount JaarInkomen =>
        _inkomens.Sum(x => (decimal)x.JaarInkomen);

    public static VerzamelInkomen Create(params Inkomen[] inkomens)
        => new(inkomens);

}