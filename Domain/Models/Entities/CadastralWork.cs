using Domain.Shared;

namespace Domain.Models.Entities;

public sealed class CadastralWork
{
    public string Id { get; }
    public string RequestId { get; }
    public string WorkDescription { get; }
    public decimal Price { get; }


    private CadastralWork(string id, string requestId, string workDescription, decimal price)
    {
        Id = id;
        RequestId = requestId;
        WorkDescription = workDescription;
        Price = price;
    }

    /// <summary>
    /// Static Factory Method
    /// </summary>
    /// <param name="workDescription"></param>
    /// <param name="price"></param>
    /// <returns>Result.Success | PriceError | EmptyField</returns>
    public static Result<CadastralWork> Create(string id, string requestId, string workDescription, decimal price)
    {
        if (string.IsNullOrWhiteSpace(workDescription))
            return Result.Failure<CadastralWork>(CommonErrors.EmptyField("Work description"));

        if (price < 0)
            return Result.Failure<CadastralWork>(CommonErrors.PriceError());

        return new CadastralWork(id , requestId,workDescription.Trim(), price);
    }
}
