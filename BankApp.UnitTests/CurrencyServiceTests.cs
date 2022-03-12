using System.Collections.Generic;
using BankApp.Data;
using BankApp.Entities;
using BankApp.Services.CurrencyService;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BankApp.UnitTests;

public class CurrencyServiceTests
{
    private readonly ICurrencyService _currencyService;
    private readonly ApplicationDbContext _dbContext;

    public CurrencyServiceTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AppDb")
            .Options;
        _dbContext = new ApplicationDbContext(dbContextOptions);
        _currencyService = new CurrencyService(_dbContext);
    }

    [Fact]
    public async void GetAllCurrenciesAsync_Always_ReturnsListOfCurrency()
    {
        // arrange

        // act
        var result = await _currencyService.GetAllCurrenciesAsync();

        // assert
        result.Should().BeOfType<List<Currency>>();
    }
}