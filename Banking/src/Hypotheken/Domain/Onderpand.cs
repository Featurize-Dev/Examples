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
