using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class CurrencyViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public string FromCurrency { get; set; } = "USD";

        [Required]
        public string ToCurrency { get; set; } = "EUR";

        public decimal ConvertedAmount { get; set; }

        // list for dropdowns
        public List<string> Currencies { get; set; } = new List<string>();
    }
}
