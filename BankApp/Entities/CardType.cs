using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Entities;

[Table("CardTypes")]
[Index(nameof(Code), IsUnique = true)]
public class CardType
{
    [Key] public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}