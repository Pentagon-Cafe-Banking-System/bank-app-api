using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("CardTypes")]
public class CardType
{
    [Key] public short Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}