using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Entities.UserTypes;

namespace BankApp.Entities;

[Table("Addresses")]
public class Address
{
    [Key] public long Id { get; set; }
    public string City { get; set; } = default!;
    public string PostCode { get; set; } = default!;
    public string Street { get; set; } = default!;

    public string CustomerId { get; set; } = default!;
    public virtual Customer Customer { get; set; } = default!;
    public virtual Country Country { get; set; } = default!;
}