namespace BankApp.Exceptions.RequestExceptions;

public class NotFoundException : AppRequestException
{
    public NotFoundException(RequestError error) : base(error)
    {
    }

    public NotFoundException(IEnumerable<dynamic> errorList) : base(errorList)
    {
    }
}