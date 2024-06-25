using Common.ValueObjects;

namespace Hypotheken.Domain;

public class Hypotheekgever
{
    public PII PII { get; set; } 
    public string Voornaam { get; set; } = string.Empty;
    public string Tussenvoegsel { get; set; } = string.Empty;
    public string Achternaam { get; set; } = string.Empty;
    public DateOnly Geboortedatum { get; set; }
    public BSN BurgerServiceNummer { get;set; }

}
