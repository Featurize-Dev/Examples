using Common.ValueObjects;
using System.Collections;

namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public List<Hypotheekgever> Hypotheekgevers { get; set; } = [];
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = Lening.Empty();
    public Onderpand Onderpand { get; set; } = Onderpand.GeenWaarde();
    public HypotheekKosten Kosten { get; } = HypotheekKosten.Create();

}

public sealed class HypotheekKosten : IEnumerable<Kosten>
{
    private readonly List<Kosten> _kosten = [];

    private HypotheekKosten()
    {
    }

    public static HypotheekKosten Create()
        => new();

    public void Add(Kosten kosten)
        => _kosten.Add(kosten);

    public void Remove(Kosten kosten) 
        => _kosten.Remove(kosten);

    public Amount Totaal 
        => _kosten.Sum(x => (decimal)x.Value);

    public IEnumerator<Kosten> GetEnumerator()
        => _kosten.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _kosten.GetEnumerator();
}

public record Kosten(string Omschrijving, Amount Value)
{
    public static Kosten NotarisKosten(Amount kosten)
        => new(nameof(NotarisKosten), kosten);

    public static Kosten Taxatiekosten(Amount kosten)
        => new(nameof(Taxatiekosten), kosten);
}