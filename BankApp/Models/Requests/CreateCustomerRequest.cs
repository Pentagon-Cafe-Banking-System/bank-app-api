namespace BankApp.Models.Requests;

public class CreateCustomerRequest
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}