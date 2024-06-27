using Common.ValueObjects;

namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public List<Hypotheekgever> Hypotheekgevers { get; set; } = [];
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = Lening.Empty();
    public Onderpand Onderpand { get; set; } = Onderpand.GeenWaarde();
    public HypotheekKosten Kosten { get; } = HypotheekKosten.Create();

    public Inkomen Inkomens { get; } = Inkomen.Verzamel(Inkomen.VastContract(65_000));

    public Percentage Lti => (decimal)Lening.Totaal / (decimal)Inkomens.JaarInkomen;
}

