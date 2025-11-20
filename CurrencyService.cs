using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyConverter.Services
{
    // Simple in-memory currency rates, base = USD
    public class CurrencyService : ICurrencyService
    {
        // Rates relative to USD (1 USD = rate * unit)
        // Example rates (you can update these values)
        private readonly Dictionary<string, decimal> _rates = new(StringComparer.OrdinalIgnoreCase)
        {
            ["USD"] = 1.0000m,
            ["EUR"] = 0.9200m,
            ["INR"] = 83.5000m,
            ["GBP"] = 0.7700m,
            ["JPY"] = 151.2000m,
            ["AUD"] = 1.4700m,
            ["CAD"] = 1.3600m
        };

        public IReadOnlyDictionary<string, decimal> GetRates() => _rates;

        public IEnumerable<string> GetCurrencyCodes() => _rates.Keys.OrderBy(k => k);

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (string.Equals(fromCurrency, toCurrency, StringComparison.OrdinalIgnoreCase))
                return Math.Round(amount, 2);

            if (!_rates.TryGetValue(fromCurrency, out var fromRate))
                throw new ArgumentException($"Unknown currency: {fromCurrency}");

            if (!_rates.TryGetValue(toCurrency, out var toRate))
                throw new ArgumentException($"Unknown currency: {toCurrency}");

            // Convert amount -> USD -> target
            // amountInUSD = amount / fromRate  (since rates are amount of currency per USD)
            // converted = amountInUSD * toRate
            decimal amountInUsd = amount / fromRate;
            decimal converted = amountInUsd * toRate;

            return Math.Round(converted, 2, MidpointRounding.AwayFromZero);
        }
    }
}
