namespace Domain.Models.Entities;

public sealed class User
{
    public string Id { get; init; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string IDNP { get; private set; } = string.Empty;
    public string Domicile { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
}
