namespace BankApp.Models;

public readonly record struct RoleType
{
    public const string Admin = "Admin";
    public const string Employee = "Employee";
    public const string Customer = "Customer";
}