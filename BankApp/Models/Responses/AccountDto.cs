namespace BankApp.Models.Responses;

public class AccountDto
{
    public long Id { get; set; }
    public string Number { get; set; } = default!;
    public decimal Balance { get; set; }
    public decimal TransferLimit { get; set; }
    public bool IsActive { get; set; }
    public string AccountTypeCode { get; set; } = default!;
    public string AccountTypeName { get; set; } = default!;
    public decimal InterestRate { get; set; }
    public string CurrencyCode { get; set; } = default!;
    public long TransferCount { get; set; }
}