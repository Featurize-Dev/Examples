using Common.ValueObjects;

namespace Hypotheken.Domain.Inkomens;

public interface IDienstverband
{

}

public class Loondienst(IDienstverband dienstverband, Amount jaarinkomen) : Inkomen
{
    public IDienstverband Dienstverband { get; } = dienstverband;

    public override DateOnly IngangsDatum => throw new NotImplementedException();

    public override DateOnly EindDatum => throw new NotImplementedException();

    public override Amount JaarInkomen => jaarinkomen;
}

public class VastContract : IDienstverband
{

}

public class TijdelijkContract : IDienstverband
{

}
