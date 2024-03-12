using Domain.Contracts;
using Domain.Models.ValueObjects;
using Domain.Shared;
using FluentDateTime;

namespace Domain.Models.Entities;

public sealed class Request : Entity
{
    public Person Client { get; private set; }
    public Person Performer { get; private set; } // the user
    public Person Responsible { get; private set; } // the user

    public DateOnly AvailableFromUtc { get; private set; }
    public DateOnly AvailableUntilUtc { get; private set; }
    public string CadastralNumber { get; private set; }
    public string Comment { get; private set; } = string.Empty;
    public RequestNumber Number { get; private set; } // generate from database, format yy/iiii
    public List<RequestState> States { get; private set; } = [];

    public List<Document> Documents { get; private set; } = [];
    public List<CadastralWork> CadastralWorks { get; private set; } = [];
    public decimal Addition { get; private set; }

    private Request(string id) : base(id) { }

    public static async Task<Result<Request>> Create(
        IRequestNumberGenerator numberGenerator,
        Person client,
        Person performer,
        Person responsible,
        string cadastralNumber,
        List<Document> documents,
        List<CadastralWork> cadastralWorks,
        DateTime availableFrom,
        string comment,
        uint deadline,
        int addition = default)
    {
        // check if person has rights for a performer
        if (performer is not { Role: RoleType.Performer | RoleType.Responsible })
            return Result.Failure<Request>(RequestErrors.PerformerRightViolation(performer.Id));

        // check if person has rights for a responsible
        if (responsible is not { Role: RoleType.Responsible })
            return Result.Failure<Request>(RequestErrors.ResponsibleRightViolation(responsible.Id));

        if (availableFrom > DateTime.UtcNow)
            return Result.Failure<Request>(RequestErrors.InvalidDateAvailableFrom());

        if (string.IsNullOrWhiteSpace(cadastralNumber))
            return Result.Failure<Request>(RequestErrors.EmptyField("Cadastral number"));

        if (deadline == 0)
            return Result.Failure<Request>(RequestErrors.InvalidDeadline());

        // if total price is negativ number return an error
        var totalPrice = addition + cadastralWorks.Sum(w => w.Price);
        if (totalPrice < 0)
            return Result.Failure<Request>(RequestErrors.PriceError());

        var request = new Request(Guid.NewGuid().ToString());

        request.Client = client;
        request.Performer = performer;
        request.Responsible = responsible;
        request.Number = await numberGenerator.GetNumberAsync();
        request.States.Add(new RequestState(Guid.NewGuid().ToString(), request.Id, StateType.Pending));
        request.Documents = documents;
        request.Addition = addition;
        request.AvailableFromUtc = GetAvailableUntil(availableFrom, deadline);
        request.AvailableUntilUtc = GetAvailableFrom(availableFrom);
        request.Comment = comment;

        return request;
    }

    public decimal GetTotalPrice() => Addition + CadastralWorks.Sum(w => w.Price);


    // need to be tesed
    public Result AddRequestState(RequestState requestState) 
    {
        var stateCreatedAt = DateOnly.FromDateTime(requestState.CreatedUtc);

        // check if state is created after the available date of request
        if (stateCreatedAt < this.AvailableFromUtc)
            return Result.Failure(RequestErrors.RequestStateError("state can't be created before the available date of the request"));
        
        // check if request is already issued
        if (requestState.State == StateType.Issued && States.Any(s => s.State == StateType.Issued))
            return Result.Failure(RequestErrors.RequestStateError("request is already issued"));

        // check if extended date is after the available date
        if (requestState.State == StateType.Extended && stateCreatedAt < AvailableUntilUtc)
            return Result.Failure(RequestErrors.RequestStateError("can't extend this request before the avalable date"));

        States.Add(requestState);
        return Result.Succes();
    }
    private static DateOnly GetAvailableUntil(DateTime availableFrom, uint deadline) => 
        DateOnly.FromDateTime(availableFrom.AddBusinessDays((int)deadline));
    private static DateOnly GetAvailableFrom(DateTime availableFrom) => DateOnly.FromDateTime(availableFrom);


}
