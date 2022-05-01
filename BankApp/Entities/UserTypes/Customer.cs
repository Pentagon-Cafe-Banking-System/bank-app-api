using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Responses;

namespace BankApp.Entities.UserTypes;

[Table("Customers")]
public class Customer
{
    [Key] [ForeignKey(nameof(AppUser))] public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = default!;
    public string FathersName { get; set; } = default!;

    public virtual AppUser AppUser { get; set; } = default!;
    public virtual Address Address { get; set; } = default!;

    public virtual List<CardOrder> BankCardOrders { get; set; } = new();

    public virtual List<Account> BankAccounts { get; set; } = new();

    public CustomerDto ToDto()
    {
        return new CustomerDto
        {
            Id = Id,
            UserName = AppUser.UserName,
            FirstName = FirstName,
            MiddleName = MiddleName,
            LastName = LastName,
            NationalId = NationalId,
            DateOfBirth = DateOfBirth,
            CityOfBirth = CityOfBirth,
            FathersName = FathersName,
            BankAccountCount = BankAccounts.Count
        };
    }
}