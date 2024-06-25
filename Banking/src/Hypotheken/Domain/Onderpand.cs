using Common.ValueObjects;

using OnderpandId = System.Guid;

namespace Hypotheken.Domain;
public sealed record Onderpand(OnderpandId Id, IOnderpandType Type, Energielabel EnergieLabel)
{
    public static Onderpand Eengezinswoning(Energielabel energielabel, Amount marktwaarde)
        => Create(new Eengezinswoning(marktwaarde), energielabel);

    public static Onderpand Create(IOnderpandType type, Energielabel energielabel)
        => new(OnderpandId.NewGuid(), type, energielabel);

    public Amount Waarde => Type.GetWaarde();

}

public interface IOnderpandType
{
    Amount GetWaarde();
}

public class Eengezinswoning(Amount marktwaarde) : IOnderpandType
{
    public Amount Marktwaarde { get; set; } = marktwaarde;

    public Amount GetWaarde()
        => Marktwaarde;
}

public class Winkel : IOnderpandType
{
    public Amount GetWaarde()
    {
        throw new NotImplementedException();
    }
}

public class EengezinswoningMetGarage : IOnderpandType
{
    public Amount GetWaarde()
    {
        throw new NotImplementedException();
    }
}