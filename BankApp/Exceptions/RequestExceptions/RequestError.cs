namespace BankApp.Exceptions.RequestExceptions;

public class RequestError
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}