namespace BankApp.Models.Responses;

public class CustomerDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string CityOfBirth { get; set; } = default!;
    public string FathersName { get; set; } = default!;
    public long BankAccountCount { get; set; }
}