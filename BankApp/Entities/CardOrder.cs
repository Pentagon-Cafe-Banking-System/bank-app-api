using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("CardOrders")]
public class CardOrder
{
    [Key] public long Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string CardType { get; set; } = string.Empty;
}