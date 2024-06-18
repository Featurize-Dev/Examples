namespace Hypotheken.Domain;

public class Lening
{
    public List<Leningdeel> Leningdelen { get; set;} = [];
    public decimal Totaal => Leningdelen.Sum(x => x.Bedrag);
}
