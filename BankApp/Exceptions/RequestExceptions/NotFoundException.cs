namespace BankApp.Exceptions.RequestExceptions;

public class NotFoundException : AppRequestException
{
    public NotFoundException(RequestError error) : base(error)
    {
    }
}