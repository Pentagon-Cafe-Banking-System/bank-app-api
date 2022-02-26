namespace BankApp.Exceptions.RequestExceptions;

public class BadRequestException : AppRequestException
{
    public BadRequestException(RequestError error) : base(error)
    {
    }

    public BadRequestException(IEnumerable<dynamic> errorList) : base(errorList)
    {
    }
}