using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Entities;

[Table("Cards")]
[Index(nameof(Number), IsUnique = true)]
public class Card
{
    [Key] public long Id { get; set; }
    public decimal TransactionLimit { get; set; }
    public DateTime ValidThru { get; set; }
    public string Number { get; set; } = default!;
    public string Cvv { get; set; } = default!;
    public string Pin { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool IsValid => IsActive && ValidThru <= DateTime.Now;

    public virtual CardType CardType { get; set; } = default!;
}