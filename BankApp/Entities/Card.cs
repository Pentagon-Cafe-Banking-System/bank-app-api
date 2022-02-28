using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("Cards")]
public class Card
{
    [Key] public long Id { get; set; }
    public decimal TransactionLimit { get; set; }
    public DateTime ValidThru { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsValid => IsActive && ValidThru <= DateTime.Now;

    public virtual CardType CardType { get; set; } = default!;
}