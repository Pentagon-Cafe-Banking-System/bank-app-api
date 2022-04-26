namespace BankApp.Exceptions.RequestErrors;

public class NotFoundError : AppRequestError
{
    public NotFoundError(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}