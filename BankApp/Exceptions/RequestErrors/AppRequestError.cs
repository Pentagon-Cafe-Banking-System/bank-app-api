using System.Text.Json;

namespace BankApp.Exceptions.RequestErrors;

public abstract class AppRequestError : Exception
{
    protected AppRequestError(string name, string description) :
        base(JsonSerializer.Serialize(new RequestError(name, description)))
    {
    }
}