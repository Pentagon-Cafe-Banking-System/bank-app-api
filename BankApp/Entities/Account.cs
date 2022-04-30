using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankApp.Entities;

[Table("Accounts")]
public class Account
{
    [Key] public long Id { get; set; }
    public string Number { get; set; } = default!;
    public decimal Balance { get; set; }
    public decimal TransferLimit { get; set; }
    public bool IsActive { get; set; }

    public virtual AccountType AccountType { get; set; } = default!;
    public virtual Currency Currency { get; set; } = default!;
    [JsonIgnore] public virtual List<Transfer> Transfers { get; set; } = default!;
}