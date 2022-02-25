using System.ComponentModel.DataAnnotations;

namespace BankApp.Entities;

public class Currency
{
    [Key] public short Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}