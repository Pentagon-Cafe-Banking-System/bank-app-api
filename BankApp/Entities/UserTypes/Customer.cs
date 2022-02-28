using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities.UserTypes;

[Table("Customers")]
public class Customer
{
    [Key] [ForeignKey("AppUser")] public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = string.Empty;
    public string FathersName { get; set; } = string.Empty;

    public virtual AppUser AppUser { get; set; } = default!;
    public virtual Address Address { get; set; } = default!;

    public virtual List<CardOrder> BankCardOrders { get; set; } = default!;

    public virtual List<Account> BankAccounts { get; set; } = default!;
}