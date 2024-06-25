namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public List<Hypotheekgever> Hypotheekgevers { get; set; } = [];
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = new Lening();

    public Onderpand Onderpand { get;set; } 

}
