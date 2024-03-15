using Domain.Models.Entities;

namespace Application.Contracts.Repositories;

internal interface ICadastralRequestRepository
{
    Task CreateCadastralRequest(CadastralRequest request);
    Task<IEnumerable<CadastralRequest>> GetCadastralRequests();
    Task<CadastralRequest> GetCadastralRequestById(string id);
    Task UpdateCadastralRequest(CadastralRequest request);
    Task DeleteCadastralRequest(string id);
}