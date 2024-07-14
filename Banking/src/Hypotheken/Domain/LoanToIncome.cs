using Common.ValueObjects;

namespace Hypotheken.Domain;

public static class LoanToIncome
{
    public static Percentage Get(Lening lening, Amount income)
        => (decimal)lening.Totaal / (decimal)income;
}