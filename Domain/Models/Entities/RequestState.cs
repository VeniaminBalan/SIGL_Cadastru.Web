using Domain.Models.ValueObjects;

namespace Domain.Models.Entities;

public sealed class RequestState : Entity
{
    public string RequestId { get; }
    public StateType State { get; }

    public RequestState(string id,string cerereId, StateType state) : base(id)
    {
        State = state;
        RequestId = cerereId;
    }
}
