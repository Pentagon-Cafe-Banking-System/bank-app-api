using BankApp.Models.Responses;
using BankApp.Services.CurrencyService;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/currency-management")]
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
    [HttpGet("currencies")]
    public async Task<ActionResult<IList<CurrencyDto>>> GetAllCurrenciesAsync()
    {
        var currencies = await _currencyService.GetAllCurrenciesAsync();
        var currenciesDto = currencies.Select(c => c.ToDto()).ToList();
        return Ok(currenciesDto);
    }
}