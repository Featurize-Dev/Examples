using Common.ValueObjects;

namespace Hypotheken.Domain.Onderpanden;

public class Eengezinswoning(Amount marktwaarde, Energielabel energielabel) : IOnderpandType
{
    public Amount Marktwaarde { get; set; } = marktwaarde;
    public Energielabel EnergieLabel { get; set; } = energielabel;

    public Amount GetWaarde()
        => Marktwaarde;
}