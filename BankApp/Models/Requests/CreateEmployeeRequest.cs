namespace BankApp.Models.Requests;

public class CreateEmployeeRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}