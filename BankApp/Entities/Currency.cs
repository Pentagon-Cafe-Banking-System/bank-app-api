using System.ComponentModel.DataAnnotations;

namespace BankApp.Entities;

public class Currency
{
    [Key] public int Id { get; set; }
    public string Code { get; set; } = default!;
    public decimal Bid { get; set; }
    public decimal Ask { get; set; }
}