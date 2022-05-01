namespace BankApp.Models.Responses;

public class CurrencyDto
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public decimal Bid { get; set; }
    public decimal Ask { get; set; }
}