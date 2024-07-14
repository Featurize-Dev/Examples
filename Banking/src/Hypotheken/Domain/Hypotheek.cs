using Common.ValueObjects;

namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public Hypotheekgever Hypotheekgever { get; set; } = Hypotheekgever.Empty();
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = Lening.Empty();
    public Onderpand Onderpand { get; set; } = OnderpandFactory.GeenWaarde();
    public Hypotheekkosten Kosten { get; } = Hypotheekkosten.Create();
    public Percentage Lti => LoanToIncome.Get(Lening, Hypotheekgever.JaarInkomen);
    public Amount MaxHypotheek => MaximaleHypotheek.Calculate(Hypotheekgever.JaarInkomen, Percentage.Create(4.5m), 30);

}
