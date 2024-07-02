using Common.ValueObjects;
using Hypotheken.Domain.Onderpanden;
using OnderpandId = System.Guid;

namespace Hypotheken.Domain;
public sealed record Onderpand(OnderpandId Id, IOnderpandType Type)
{
   public Amount Waarde => Type.GetWaarde();

}

public static class OnderpandFactory
{
    public static Onderpand Eengezinswoning(Energielabel energielabel, Amount marktwaarde)
       => Create(new Eengezinswoning(marktwaarde, energielabel));

    public static Onderpand GeenWaarde()
        => Create(new GeenWaarde());

    public static Onderpand Create(IOnderpandType type)
        => new(OnderpandId.NewGuid(), type);
}

public class GeenWaarde : IOnderpandType
{
    public Amount GetWaarde() => 0;
}

public interface IOnderpandType
{
    Amount GetWaarde();
}
