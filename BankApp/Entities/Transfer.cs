using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankApp.Models.Responses;

namespace BankApp.Entities;

[Table("Transfers")]
public class Transfer
{
    [Key] public long Id { get; set; }
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = default!;
    public string ReceiverName { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateTime Ordered { get; set; }
    public DateTime Executed { get; set; }
    public string? ReasonFailed { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }

    public long SenderAccountId { get; set; }
    public virtual Account SenderAccount { get; set; } = default!;

    public TransferDto ToDto()
    {
        return new TransferDto
        {
            Id = Id,
            Amount = Amount,
            ReceiverAccountNumber = ReceiverAccountNumber,
            ReceiverName = ReceiverName,
            Title = Title,
            Ordered = Ordered,
            Executed = Executed,
            ReasonFailed = ReasonFailed,
            IsCompleted = IsCompleted,
            IsFailed = IsFailed,
            SenderAccountNumber = SenderAccount.Number,
            SenderAccountCurrencyCode = SenderAccount.Currency.Code
        };
    }
}