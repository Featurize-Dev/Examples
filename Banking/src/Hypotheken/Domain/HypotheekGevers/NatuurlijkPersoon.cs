using Common.ValueObjects;

namespace Hypotheken.Domain.HypotheekGevers;
public class NatuurlijkPersoon : Hypotheekgever
{
    public string Voornaam { get; set; } = string.Empty;
    public string Tussenvoegsel { get; set; } = string.Empty;
    public string Achternaam { get; set; } = string.Empty;
    public DateOnly Geboortedatum { get; set; }
    public BSN BurgerServiceNummer { get; set; }

    public Inkomen Inkomens { get; } = InkomenFactory.Verzamel(InkomenFactory.VastContract(65_000));

    public override Amount JaarInkomen => Inkomens.JaarInkomen;
}
