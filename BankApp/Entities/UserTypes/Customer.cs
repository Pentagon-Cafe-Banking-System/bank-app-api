using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankApp.Entities.UserTypes;

public class Customer
{
    [Key] public string Id { get; set; } = string.Empty;
    [JsonIgnore] public virtual string AppUserId { get; set; } = default!;
    public virtual AppUser AppUser { get; set; } = default!;
}