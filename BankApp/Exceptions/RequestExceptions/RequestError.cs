namespace BankApp.Exceptions.RequestExceptions;

public class RequestError
{
    public RequestError(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public Dictionary<string, string> GetError()
    {
        var errors = new Dictionary<string, string> {{Name, Description}};
        return errors;
    }
}