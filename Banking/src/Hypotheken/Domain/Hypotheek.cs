namespace Hypotheken.Domain;
public class HypotheekAkte
{ 
    public List<Hypotheekgever> Hypotheekgevers { get; set; } = [];
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = new Lening();

}
