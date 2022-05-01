using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankApp.Entities;

[Table("AccountTypeCurrencies")]
public class AccountTypeCurrency
{
    [Key] public int AccountTypeId { get; set; }
    [JsonIgnore] public virtual AccountType AccountType { get; set; } = default!;

    [Key] public int CurrencyId { get; set; }
    public virtual Currency Currency { get; set; } = default!;
}