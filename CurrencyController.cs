using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Models;
using CurrencyConverter.Services;
using System.Linq;

namespace CurrencyConverter.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new CurrencyViewModel
            {
                Amount = 1,
                FromCurrency = "USD",
                ToCurrency = "EUR",
                Currencies = _currencyService.GetCurrencyCodes().ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CurrencyViewModel vm)
        {
            vm.Currencies = _currencyService.GetCurrencyCodes().ToList();

            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                vm.ConvertedAmount = _currencyService.Convert(vm.Amount, vm.FromCurrency, vm.ToCurrency);
            }
            catch (System.ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(vm);
        }
    }
}
