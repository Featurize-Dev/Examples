using Common.ValueObjects;
using System.Collections;
using static Hypotheken.Domain.Hypotheekkosten;

namespace Hypotheken.Domain;

public sealed class Hypotheekkosten : IEnumerable<Kosten>
{
    private readonly List<Kosten> _kosten = [];

    private Hypotheekkosten()
    {
    }

    public static Hypotheekkosten Create()
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

    public record Kosten(string Omschrijving, Amount Value)
    {
        public static Kosten NotarisKosten(Amount kosten)
            => new(nameof(NotarisKosten), kosten);

        public static Kosten Taxatiekosten(Amount kosten)
            => new(nameof(Taxatiekosten), kosten);
    }
}

