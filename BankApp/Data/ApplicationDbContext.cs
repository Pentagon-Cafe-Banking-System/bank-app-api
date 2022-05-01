using BankApp.Entities;
using BankApp.Entities.UserTypes;
using BankApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Employee> Employees { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<AccountType> AccountTypes { get; set; } = default!;
    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<Card> Cards { get; set; } = default!;
    public DbSet<CardOrder> CardOrders { get; set; } = default!;
    public DbSet<CardType> CardTypes { get; set; } = default!;
    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Currency> Currencies { get; set; } = default!;
    public DbSet<Transfer> Transfers { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        DisableNotNeededIdentityFeatures(builder);
        ConfigureRelationships(builder);

        const string adminRoleId = "fa2640a0-0496-4010-bc27-424e0e5c6f78";
        SeedRoles(builder, adminRoleId);
        CreateAdminAccount(builder, adminRoleId);

        SeedCountries(builder);

        SeedCurrencies(builder);
        SeedAccountTypes(builder);
    }

    private void DisableNotNeededIdentityFeatures(ModelBuilder builder)
    {
        // Only needed for login via 3rd party account
        builder.Entity<IdentityUserToken<string>>().Metadata.SetIsTableExcludedFromMigrations(true);
        builder.Entity<IdentityUserLogin<string>>().Metadata.SetIsTableExcludedFromMigrations(true);

        // Disable IdentityUser attributes
        builder.Entity<AppUser>().Ignore(c => c.PhoneNumber);
        builder.Entity<AppUser>().Ignore(c => c.PhoneNumberConfirmed);
        builder.Entity<AppUser>().Ignore(c => c.Email);
        builder.Entity<AppUser>().Ignore(c => c.NormalizedEmail);
        builder.Entity<AppUser>().Ignore(c => c.EmailConfirmed);
    }

    private void ConfigureRelationships(ModelBuilder builder)
    {
        ConfigureRelationshipsWithAppUser(builder);

        builder.Entity<AccountTypeCurrency>()
            .HasKey("AccountTypeId", nameof(AccountTypeCurrency.CurrencyId));
    }

    private void ConfigureRelationshipsWithAppUser(ModelBuilder builder)
    {
        builder.Entity<Admin>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Employee>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Customer>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void SeedRoles(ModelBuilder builder, string adminRoleId)
    {
        var roles = new List<IdentityRole>
        {
            new() {Name = RoleType.Admin, NormalizedName = RoleType.Admin.ToUpper(), Id = adminRoleId},
            new() {Name = RoleType.Employee, NormalizedName = RoleType.Employee.ToUpper()},
            new() {Name = RoleType.Customer, NormalizedName = RoleType.Customer.ToUpper()}
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }

    private void CreateAdminAccount(ModelBuilder builder, string adminRoleId)
    {
        const string adminUserId = "7a4165b4-0aca-43fb-a390-294781ee377f";
        var hasher = new PasswordHasher<AppUser>();
        builder.Entity<AppUser>().HasData(
            new AppUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                PasswordHash = hasher.HashPassword(null!, "admin")
            }
        );
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            }
        );
        builder.Entity<Admin>().HasData(
            new Admin
            {
                Id = adminUserId
            }
        );
    }

    private void SeedCountries(ModelBuilder builder)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "countries.csv");
        var lines = File.ReadLines(path);
        int countryId = 1;
        foreach (var line in lines)
        {
            var arr = line.Split(",");
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = countryId++,
                    Code = arr[0],
                    Name = arr[3]
                }
            );
        }
    }

    private void SeedCurrencies(ModelBuilder builder)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "available_currency_codes.csv");
        var line = File.ReadAllLines(path).First();
        var currencyCodes = line.Split(";");
        int currencyId = 1;
        foreach (var currencyCode in currencyCodes)
        {
            builder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = currencyId++,
                    Code = currencyCode,
                    Bid = default!,
                    Ask = default!
                }
            );
        }
    }

    private void SeedAccountTypes(ModelBuilder builder)
    {
        builder.Entity<AccountType>().HasData(
            new AccountType
            {
                Id = 1,
                Code = "CA",
                Name = "Current Account",
                InterestRate = (decimal) 0.5
            },
            new AccountType
            {
                Id = 2,
                Code = "SA",
                Name = "Savings Account",
                InterestRate = (decimal) 3.0
            },
            new AccountType
            {
                Id = 3,
                Code = "FCA",
                Name = "Foreign Currency Account",
                InterestRate = (decimal) 0.0
            }
        );

        builder.Entity<AccountTypeCurrency>().HasData(
            new AccountTypeCurrency
            {
                AccountTypeId = 1,
                CurrencyId = 1
            },
            new AccountTypeCurrency
            {
                AccountTypeId = 2,
                CurrencyId = 1
            }
        );
        foreach (var currencyId in Enumerable.Range(2, 13))
        {
            builder.Entity<AccountTypeCurrency>().HasData(
                new AccountTypeCurrency
                {
                    AccountTypeId = 3,
                    CurrencyId = currencyId
                }
            );
        }
    }
}