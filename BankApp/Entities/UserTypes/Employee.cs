using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankApp.Entities.UserTypes;

public class Employee
{
    [Key] [JsonIgnore] public Int64 Id { get; set; }
    public virtual string AppUserId { get; set; } = default!;
    public virtual AppUser AppUser { get; set; } = default!;
}