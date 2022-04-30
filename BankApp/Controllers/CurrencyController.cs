using BankApp.Entities;
using BankApp.Services.CurrencyService;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/currencies")]
[ApiExplorerSettings(GroupName = "Currencies")]
public class CurrencyController : Controller
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    /// <summary>
    /// Returns all available currencies with their rates.
    /// </summary>
    [HttpGet]
    public async Task<IEnumerable<Currency>> GetAllCurrencies()
    {
        var currencies = await _currencyService.GetAllCurrencies();
        return currencies;
    }
}