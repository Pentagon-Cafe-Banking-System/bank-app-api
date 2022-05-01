using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("AccountTypes")]
public class AccountType
{
    [Key] public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal InterestRate { get; set; }

    public virtual List<AccountTypeCurrency> AvailableCurrencies { get; set; } = default!;
}