using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Eengezinswoning(Amount marktwaarde) : IOnderpandType
{
    public Amount Marktwaarde { get; set; } = marktwaarde;

    public Amount GetWaarde()
        => Marktwaarde;
}