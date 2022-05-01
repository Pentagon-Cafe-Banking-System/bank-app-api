using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("Countries")]
public class Country
{
    [Key] public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
}