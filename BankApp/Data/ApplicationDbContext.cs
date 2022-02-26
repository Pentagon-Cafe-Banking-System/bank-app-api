﻿using BankApp.Entities;
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
    }

    private void DisableNotNeededIdentityFeatures(ModelBuilder builder)
    {
        // Only needed for login via 3rd party account
        builder.Entity<IdentityUserToken<string>>().Metadata.SetIsTableExcludedFromMigrations(true);
        builder.Entity<IdentityUserLogin<string>>().Metadata.SetIsTableExcludedFromMigrations(true);
    }

    private void ConfigureRelationships(ModelBuilder builder)
    {
        ConfigureRelationshipsWithAppUser(builder);
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
            new() {Name = RoleType.Admin, NormalizedName = RoleType.Admin.ToUpperInvariant(), Id = adminRoleId},
            new() {Name = RoleType.Employee, NormalizedName = RoleType.Employee.ToUpperInvariant()},
            new() {Name = RoleType.Customer, NormalizedName = RoleType.Customer.ToUpperInvariant()}
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
                NormalizedUserName = "admin".ToUpperInvariant(),
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
        var lines = File.ReadAllLines(path);
        short countryId = 1;
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
}