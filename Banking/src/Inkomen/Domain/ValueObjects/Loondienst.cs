using Common.ValueObjects;

namespace Inkomen.Domain.ValueObjects;

public record Werknemer(string Voornaam, string Achternaam)
{
    public static Werknemer Empty() 
        => new(string.Empty, string.Empty);
}

public record Werkgever(string Naam, Adres Adres)
{
    public static Werkgever Empty() 
        => new("?", Adres.Empty());
}

public record Arbeidsvoorwaarden(Amount Salaris, int Arbeidsduur, int VakantieDagen)
{
    public static Arbeidsvoorwaarden Empty() 
        => new(0, 0, 0);
};

public enum Overeenkomst
{
    BepaaldeTijd = 1,
    OnbepaaldeTijd = 2
}

