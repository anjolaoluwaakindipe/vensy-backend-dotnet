namespace Vensy.Domain.Models;

public class Company {
    public int Id { get; set;}
    public string Name { get; set;} = string.Empty;
    public string Email { get; set;} = string.Empty;

    public string Phone { get; set;} = string.Empty;

    public string Address { get; set;} = string.Empty;

    public int UserId { get; set;}

    public User User {get; set;} = null!;


    public List<Venue> Venues { get;} = new List<Venue>();


    public override string ToString()
    {
        var stringProperties = from prop in GetType().GetProperties()
                               where prop.Name != "User"
                               select $"\t {prop.Name}: {prop.GetValue(this)}";

        return $"Company: {{\n {string.Join("\n", stringProperties)} \n}}";
    }
}