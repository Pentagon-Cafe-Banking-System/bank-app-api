using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Entities.UserTypes;

namespace BankApp.Entities;

[Table("Accounts")]
public class Account
{
    [Key] public long Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal TransferLimit { get; set; }
    public bool IsActive { get; set; }

    public virtual AccountType AccountType { get; set; } = default!;
    public virtual Currency Currency { get; set; } = default!;
    public virtual List<Transfer> Transfers { get; set; } = default!;
}