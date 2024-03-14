using System.ComponentModel.DataAnnotations;
using Domain.Shared;

namespace Domain.Models.Entities.Clients;

public sealed class Client
{
    public string Id { get; init; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string IDNP { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string Domicile { get; private set; } = string.Empty; 
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }

    private Client(string id) { }
    private Client() { }

    public static async Task<Result<Client>> Create(
        IIdnpUnique idnpUniqueServie,
        string firstname,
        string lastname,
        string idnp,
        DateTime dateOfBirth,
        string domicile,
        string? email = default,
        string? phoneNumber = default) 
    {
        if(email is not null && !isEmailValid(email))
            return Result.Failure<Client>(CommonErrors.InvalidEmail("Email"));

        if(string.IsNullOrWhiteSpace(firstname))
            return Result.Failure<Client>(CommonErrors.EmptyField("First name"));
        
        if(string.IsNullOrWhiteSpace(lastname))
            return Result.Failure<Client>(CommonErrors.EmptyField("Last name"));

        if(string.IsNullOrWhiteSpace(idnp))
            return Result.Failure<Client>(CommonErrors.EmptyField("IDNP"));

        if(string.IsNullOrWhiteSpace(domicile))
            return Result.Failure<Client>(CommonErrors.EmptyField("Domicile"));

        if(!await idnpUniqueServie.IsIdnpUniqueAsunc(idnp))
            return Result.Failure<Client>(CommonErrors.NotUnique("IDNP"));

        //check idnp
        //check email
        //validate fields

        return new Client
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = firstname,
            LastName = lastname,
            IDNP = idnp,
            DateOfBirth = dateOfBirth,
            Domicile = domicile,
            Email = email,
            PhoneNumber = phoneNumber
        };
    }

    private static bool isEmailValid(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}
