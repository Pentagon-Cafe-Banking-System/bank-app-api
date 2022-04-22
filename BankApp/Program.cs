using System.Reflection;
using System.Text;
using BankApp.Data;
using BankApp.Entities.UserTypes;
using BankApp.Middleware;
using BankApp.Models.Requests;
using BankApp.Services;
using BankApp.Services.AccountService;
using BankApp.Services.AccountTypeService;
using BankApp.Services.AuthService;
using BankApp.Services.CurrencyService;
using BankApp.Services.CustomerService;
using BankApp.Services.EmployeeService;
using BankApp.Services.JwtService;
using BankApp.Services.TransferService;
using BankApp.Services.UserService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    // Set the comments path for the Swagger JSON and UI.
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
            return new[] {api.GroupName};
        if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            return new[] {controllerActionDescriptor.ControllerName};

        throw new InvalidOperationException("Unable to determine tag for endpoint.");
    });
    options.DocInclusionPredicate((name, api) => true);
});

const string corsPolicy = "DefaultPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        policyBuilder =>
        {
            policyBuilder.WithOrigins(
                    "http://localhost:3000", "https://localhost:3000",
                    "https://bank-app-pcafe.herokuapp.com"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:JwtSecret").Value);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // Timezone problem workaround
            LifetimeValidator = (notBefore, expires, _, _) =>
            {
                if (notBefore is not null)
                    return notBefore.Value.AddMinutes(-5) <= DateTime.UtcNow && expires >= DateTime.UtcNow;
                return expires >= DateTime.UtcNow;
            }
        };
    });

string GetHerokuConnectionString()
{
    var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (connectionUrl == null)
        return string.Empty; // temporary solution for integration tests to work

    var databaseUri = new Uri(connectionUrl);
    var db = databaseUri.LocalPath.TrimStart('/');
    var userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
    return $"UserID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
           $"Database={db};Pooling=true;SSLMode=Require;TrustServerCertificate=True;";
}

var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
var connectionString = isDevelopment ? builder.Configuration.GetConnectionString("AppDb") : GetHerokuConnectionString();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<AppUser>(options =>
    {
        options.Password = new PasswordOptions
        {
            RequireDigit = false,
            RequiredLength = 0,
            RequireLowercase = false,
            RequireUppercase = false,
            RequiredUniqueChars = 0,
            RequireNonAlphanumeric = false,
        };
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<ExceptionHandlerMiddleware>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountTypeService, AccountTypeService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddFluentValidation();
builder.Services.AddScoped<IValidator<CreateEmployeeRequest>, CreateEmployeeRequestValidator>();
builder.Services.AddScoped<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateEmployeeRequest>, UpdateEmployeeRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
builder.Services.AddScoped<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateAccountRequest>, UpdateAccountRequestValidator>();
builder.Services.AddScoped<IValidator<CreateTransferRequest>, CreateTransferRequestValidator>();

builder.Services.AddHostedService<ExchangeRatesService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

// if (app.Environment.IsDevelopment())
//     app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}