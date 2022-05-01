using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BankApp.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Entities;

[Table("Accounts")]
[Index(nameof(Number), IsUnique = true)]
public class Account
{
    [Key] public long Id { get; set; }
    public string Number { get; set; } = default!;
    public decimal Balance { get; set; }
    public decimal TransferLimit { get; set; }
    public bool IsActive { get; set; }

    public virtual AccountType AccountType { get; set; } = default!;
    public virtual Currency Currency { get; set; } = default!;
    [JsonIgnore] public virtual List<Transfer> Transfers { get; set; } = new();

    public AccountDto ToDto()
    {
        return new AccountDto
        {
            Id = Id,
            Number = Number,
            Balance = Balance,
            TransferLimit = TransferLimit,
            IsActive = IsActive,
            AccountTypeCode = AccountType.Code,
            AccountTypeName = AccountType.Name,
            InterestRate = AccountType.InterestRate,
            CurrencyCode = Currency.Code,
            TransferCount = Transfers.Count
        };
    }
}