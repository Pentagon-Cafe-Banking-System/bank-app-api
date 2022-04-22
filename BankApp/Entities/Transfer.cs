using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Entities;

[Table("Transfers")]
public class Transfer
{
    [Key] public long Id { get; set; }
    public decimal Amount { get; set; }
    public string ReceiverAccountNumber { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Ordered { get; set; }
    public DateTime Executed { get; set; }
    public string? ReasonFailed { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsFailed { get; set; }

    public long SenderAccountId { get; set; }
    public virtual Account SenderAccount { get; set; } = default!;
}