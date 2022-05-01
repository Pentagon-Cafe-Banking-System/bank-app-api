namespace BankApp.Models.Responses;

public class AccountTypeDto
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public decimal InterestRate { get; set; }
}