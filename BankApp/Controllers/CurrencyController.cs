using BankApp.Entities;
using BankApp.Services.CurrencyService;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : Controller
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<IEnumerable<Currency>> GetAllCurrencies()
    {
        return await _currencyService.GetAllCurrenciesAsync();
    }
}