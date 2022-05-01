using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Responses;

namespace BankApp.Entities.UserTypes;

[Table("Employees")]
public class Employee
{
    [Key] [ForeignKey(nameof(AppUser))] public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public double Salary { get; set; }
    public char Gender { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual AppUser AppUser { get; set; } = default!;

    public EmployeeDto ToDto()
    {
        return new EmployeeDto
        {
            Id = Id,
            UserName = AppUser.UserName,
            FirstName = FirstName,
            LastName = LastName,
            Salary = Salary,
            Gender = Gender,
            DateOfEmployment = DateOfEmployment,
            DateOfBirth = DateOfBirth
        };
    }
}