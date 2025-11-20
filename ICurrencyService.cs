using System.Collections.Generic;

namespace CurrencyConverter.Services
{
    public interface ICurrencyService
    {
        IReadOnlyDictionary<string, decimal> GetRates();
        decimal Convert(decimal amount, string fromCurrency, string toCurrency);
        IEnumerable<string> GetCurrencyCodes();
    }
}
