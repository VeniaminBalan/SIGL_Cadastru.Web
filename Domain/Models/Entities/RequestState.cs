using Domain.Models.ValueObjects;
namespace Domain.Models.Entities;

public sealed class RequestState
{
    public string Id { get; }
    public string RequestId { get; }
    public StateType State { get; }
    public DateTime CreatedUtc { get; }

    public RequestState(string id,string requestId, StateType state)
    {
        Id = id;
        State = state;
        RequestId = requestId;
        CreatedUtc = DateTime.UtcNow;
    }
}
