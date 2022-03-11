using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BankApp.Data;
using BankApp.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace BankApp.IntegrationTests;

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JwtServiceMock _jwtServiceMock;

    public UserControllerTests(WebApplicationFactory<Program> factory)
    {
        _jwtServiceMock = new JwtServiceMock();
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(IHostedService));

                // Database
                var dbContextOptions = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (dbContextOptions is not null)
                    services.Remove(dbContextOptions);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDb"));

                // Authentication
                services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _jwtServiceMock.SecurityKey,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            });
        });
    }

    [Fact]
    public async Task GetAllUsers_ForAdmin_ReturnsOkResult()
    {
        // arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, "Admin")
        };
        var token = _jwtServiceMock.GenerateJwtToken(claims);

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

        // act
        var response = await client.GetAsync("/api/User");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("Employee")]
    [InlineData("Customer")]
    public async Task GetAllUsers_ForRolesOtherThanAdmin_ReturnsForbiddenResult(string roleName)
    {
        // arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, roleName)
        };
        var token = _jwtServiceMock.GenerateJwtToken(claims);

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");

        // act
        var response = await client.GetAsync("/api/User");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}