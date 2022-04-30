using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities.UserTypes;

[Table("Customers")]
public class Customer
{
    [Key] [ForeignKey("AppUser")] public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = default!;
    public string FathersName { get; set; } = default!;

    public virtual AppUser AppUser { get; set; } = default!;
    public virtual Address Address { get; set; } = default!;

    public virtual List<CardOrder> BankCardOrders { get; set; } = default!;

    public virtual List<Account> BankAccounts { get; set; } = default!;
}