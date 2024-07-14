using Common.ValueObjects;

namespace Hypotheken.Domain;

public record WoonQuote(Amount Amount, Percentage Percentage)
{
    public static Percentage Get(Amount jaarInkomen)
        => Get(DateTime.Now.Year, jaarInkomen);
    public static Percentage Get(int jaar, Amount jaarinkomen)
        => jaar switch
        {
            2024 => Woonquote_Table_2024.Last(x=>x.Amount <= jaarinkomen).Percentage,
            _ => Woonquote_Table_2024.Last(x => x.Amount <= jaarinkomen).Percentage
        };

    public static List<WoonQuote> Woonquote_Table_2024 = [
        new(29_000, 17.5m),
        new(30_000, 29m),
        new(31_000, 20.5m),
        new(32_000, 21.5m),
        new(33_000, 22m),
        new(35_000, 22.5m),
        new(37_000, 23m),
        new(38_000, 23.5m),
        new(56_000, 24m),
        new(61_000, 24.5m),
        new(67_000, 25m),
        new(70_000, 26m),
        new(73_000, 26.5m),
        new(77_000, 27m),
        new(85_000, 27.5m),
        new(95_000, 27.5m),
        new(105_000, 28.5m),
        new(Amount.Max, 29m),
    ];
};


