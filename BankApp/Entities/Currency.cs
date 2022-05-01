using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Entities;

[Table("Currencies")]
[Index(nameof(Code), IsUnique = true)]
public class Currency
{
    [Key] public int Id { get; set; }
    public string Code { get; set; } = default!;
    public decimal Bid { get; set; }
    public decimal Ask { get; set; }

    public CurrencyDto ToDto()
    {
        return new CurrencyDto
        {
            Id = Id,
            Code = Code,
            Bid = Bid,
            Ask = Ask
        };
    }
}