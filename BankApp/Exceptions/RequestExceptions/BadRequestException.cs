namespace BankApp.Exceptions.RequestExceptions;

public class BadRequestException : AppRequestException
{
    public BadRequestException(RequestError error) : base(error)
    {
    }
}