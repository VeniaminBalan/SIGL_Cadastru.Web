namespace Domain.Models.Entities.Clients;

public interface IIdnpUnique
{
    bool IsIdnpUnique(string idnp);
    Task<bool> IsIdnpUniqueAsunc(string idnp);
}
