using Domain.Models.ValueObjects;

namespace Domain.Contracts;

public interface IRequestNumberGenerator
{
    Task<RequestNumber> GetNumberAsync();
    RequestNumber GetNumber();
}
