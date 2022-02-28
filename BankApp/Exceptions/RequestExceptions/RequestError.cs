namespace BankApp.Exceptions.RequestExceptions;

public class RequestError
{
    public RequestError(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public List<string> Descriptions { get; set; } = new();

    public RequestError Add(string description)
    {
        Descriptions.Add(description);
        return this;
    }

    public Dictionary<string, List<string>> Get()
    {
        var dict = new Dictionary<string, List<string>> {{Name, Descriptions}};
        return dict;
    }
}