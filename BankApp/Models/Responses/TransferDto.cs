namespace BankApp.Models.Responses;

public class TransferDto
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = default!;
    public string ReceiverName { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateTime Ordered { get; set; }
    public DateTime Executed { get; set; }
    public string? ReasonFailed { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }
    public string SenderAccountNumber { get; set; } = default!;
    public string SenderAccountCurrencyCode { get; set; } = default!;
}