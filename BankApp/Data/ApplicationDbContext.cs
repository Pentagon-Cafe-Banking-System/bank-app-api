using BankApp.Entities.UserTypes;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Only needed for login via 3rd party account
        builder.Entity<IdentityUserToken<string>>().Metadata.SetIsTableExcludedFromMigrations(true);
        builder.Entity<IdentityUserLogin<string>>().Metadata.SetIsTableExcludedFromMigrations(true);

        // Create roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole {Name = "Employee", NormalizedName = "EMPLOYEE"},
            new IdentityRole {Name = "Customer", NormalizedName = "CUSTOMER"}
        );

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
}