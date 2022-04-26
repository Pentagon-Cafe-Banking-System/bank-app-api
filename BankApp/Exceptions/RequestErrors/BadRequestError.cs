namespace BankApp.Exceptions.RequestErrors;

public class BadRequestError : AppRequestError
{
    public BadRequestError(string name, string description) : base(name, description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}