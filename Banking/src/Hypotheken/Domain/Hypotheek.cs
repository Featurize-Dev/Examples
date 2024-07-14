using Common.ValueObjects;
using System.ComponentModel;

namespace Hypotheken.Domain;
public class Hypotheek
{ 
    public Hypotheekgever Hypotheekgever { get; set; } = Hypotheekgever.Empty();
    public Hypotheeknemer Hypotheeknemer { get;set; } = Hypotheeknemer.GoldCreditBank;
    public Lening Lening { get; set; } = Lening.Empty();
    public Onderpand Onderpand { get; set; } = OnderpandFactory.GeenWaarde();
    public Hypotheekkosten Kosten { get; } = Hypotheekkosten.Create();
    public Percentage Lti => (decimal)Lening.Totaal / (decimal)Hypotheekgever.JaarInkomen;
}


