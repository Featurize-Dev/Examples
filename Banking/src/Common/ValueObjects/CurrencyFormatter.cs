using System.Globalization;

namespace Common.ValueObjects;

public class CurrencyFormatter
{
    public static string FormatCurrency(Currency currency, Amount amount, int decPlaces)
    {
        NumberFormatInfo localFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        localFormat.CurrencySymbol = currency.Symbol;
        localFormat.CurrencyDecimalDigits = decPlaces;
        return ((decimal)amount).ToString("c", localFormat);
    }
}