namespace Domain.Models.Entities;

public sealed class Person : Entity
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string IDNP { get; private set; } = string.Empty;
    public DateOnly DateOfBirth { get; private set; }
    public string Domicile { get; private set; } = string.Empty; 
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public RoleType Role { get; private set; }

    private Person(string id) : base(id){ }
    private Person() : base(Guid.NewGuid().ToString()) { }

    public static Person Create(
        string firstname,
        string lastname,
        string idnp,
        DateOnly dateOfBirth,
        string domicile,
        string? email = default,
        string? phoneNumber = default,
        RoleType role = RoleType.Client) 
    {
        //check idnp
        //check email
        //validate fields

        return new Person
        {

        };
    }
}
