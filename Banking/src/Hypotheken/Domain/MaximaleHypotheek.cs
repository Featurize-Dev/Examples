using Common.ValueObjects;

namespace Hypotheken.Domain;

public static class MaximaleHypotheek
{
    public static Amount Calculate(Amount toetsInkomen, Percentage toetsRente, int looptijd)
    {
        var woonquote = WoonQuote.Get(toetsInkomen);
        var maxwoonlasten = toetsInkomen * woonquote;
        return maxwoonlasten / toetsRente;
    }
}


