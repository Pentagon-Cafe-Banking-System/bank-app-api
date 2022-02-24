namespace BankApp.Entities.UserTypes;

public readonly record struct RoleType
{
    public const string Admin = "Admin";
    public const string Employee = "Employee";
    public const string Customer = "Customer";
}