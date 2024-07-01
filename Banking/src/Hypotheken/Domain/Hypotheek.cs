using Common.ValueObjects;

namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public List<Hypotheekgever> Hypotheekgevers { get; set; } = [];
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = Lening.Empty();
    public Onderpand Onderpand { get; set; } = Onderpand.GeenWaarde();
    public Hypotheekkosten Kosten { get; } = Hypotheekkosten.Create();

    public Inkomen Inkomens { get; } = InkomenFactory.Verzamel(InkomenFactory.VastContract(65_000));

    public Percentage Lti => (decimal)Lening.Totaal / (decimal)Inkomens.JaarInkomen;
}

