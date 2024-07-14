using Common.ValueObjects;

namespace Hypotheken.Domain.HypotheekGevers;
public class Samengesteld : Hypotheekgever
{
    private List<Hypotheekgever> _hypotheekgevers = [];
    public Samengesteld(params Hypotheekgever[] hypotheekgevers)
    {
        _hypotheekgevers.AddRange(hypotheekgevers);
    }

    public override Amount JaarInkomen => _hypotheekgevers.Sum(x => x.JaarInkomen);
}
