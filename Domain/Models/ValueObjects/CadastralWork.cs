using Domain.Shared;

namespace Domain.Models.ValueObjects;

public record struct CadastralWork
{
    public string WorkDescription { get; }
    public decimal Price { get; }

    private CadastralWork(string workDescription, decimal price)
    {
        WorkDescription = workDescription;
        Price = price;
    }

    /// <summary>
    /// Static Factory Method
    /// </summary>
    /// <param name="workDescription"></param>
    /// <param name="price"></param>
    /// <returns>Result.Succes | PriceError | EmptyField</returns>
    public static Result<CadastralWork> Create(string workDescription, decimal price) 
    {
        if (string.IsNullOrWhiteSpace(workDescription))
            return Result.Failure<CadastralWork>(CadastralWorkErrors.EmptyField("Work description"));

        if(price < 0)
            return Result.Failure<CadastralWork>(CadastralWorkErrors.PriceError());

        return new CadastralWork(workDescription.Trim(), price);
    }
}
