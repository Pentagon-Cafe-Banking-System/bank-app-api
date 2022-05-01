namespace BankApp.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}