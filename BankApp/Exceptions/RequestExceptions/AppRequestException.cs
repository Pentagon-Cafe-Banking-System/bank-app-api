using System.Text.Json;
using BankApp.Exceptions.RequestExceptions;

namespace BankApp.Exceptions;

public abstract class AppRequestException : Exception
{
    protected AppRequestException(RequestError error) : base(JsonSerializer.Serialize(error))
    {
    }

    protected AppRequestException(IEnumerable<dynamic> errorList) : base(JsonSerializer.Serialize(errorList))
    {
    }
}