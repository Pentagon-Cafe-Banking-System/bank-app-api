using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Responses;

namespace BankApp.Entities.UserTypes;

[Table("Admins")]
public class Admin
{
    [Key] [ForeignKey(nameof(AppUser))] public string Id { get; set; } = default!;
    public virtual AppUser AppUser { get; set; } = default!;
    
    public AdminDto ToDto()
    {
        return new AdminDto
        {
            Id = Id,
            UserName = AppUser.UserName,
        };
    }
}