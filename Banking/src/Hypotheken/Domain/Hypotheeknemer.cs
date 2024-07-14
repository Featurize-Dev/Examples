using Common.ValueObjects;
using Featurize.ValueObjects;

namespace Hypotheken.Domain;

public record Hypotheeknemer(string Naam, Adres[] Adressen)
{
    public static Hypotheeknemer GoldCreditBank => new("GoldCreditBank", [
        new Adres("Bezuidenhoutseweg", "16B", "2594 AV", "Den Haag", Country.Parse("NL")),
        new Adres("Postbus", "2687", "3800 GE", "Amerfoort", Country.Parse("NL")),
        ]);
}
