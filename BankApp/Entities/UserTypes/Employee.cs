using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities.UserTypes;

[Table("Employees")]
public class Employee
{
    [Key] [ForeignKey("AppUser")] public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public double Salary { get; set; }
    public char Gender { get; set; }
    public DateOnly DateOfEmployment { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public virtual AppUser AppUser { get; set; } = default!;
}