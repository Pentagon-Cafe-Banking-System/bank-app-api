using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities.UserTypes;

public class Admin
{
    [Key] [ForeignKey("AppUser")] public string Id { get; set; } = string.Empty;
    public virtual AppUser AppUser { get; set; } = default!;
}