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

        // Create relationships
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

        // Create roles

        const string adminRoleId = "fa2640a0-0496-4010-bc27-424e0e5c6f78";
        const string adminUserId = "7a4165b4-0aca-43fb-a390-294781ee377f";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
                {Name = RoleType.Admin, NormalizedName = RoleType.Admin.ToUpperInvariant(), Id = adminRoleId},
            new IdentityRole {Name = RoleType.Employee, NormalizedName = RoleType.Employee.ToUpperInvariant()},
            new IdentityRole {Name = RoleType.Customer, NormalizedName = RoleType.Customer.ToUpperInvariant()}
        );

        // Create Admin account

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
}