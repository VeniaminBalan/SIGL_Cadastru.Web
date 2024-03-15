

namespace Domain.Shared;

public record Error(ErrorType Type, string Description = null)
{
    public static readonly Error None = new(ErrorType.None, string.Empty);
    public static implicit operator Result(Error error) => Result.Failure(error);
}

public enum ErrorType
{
    None,
    Dublicate,
    NotFound,
    BadRequest,
    InternalServerError
}


public class CommonErrors 
{
    public static Error PriceError() => new Error(ErrorType.BadRequest, "Price can't be negative number");
    public static Error EmptyField(string field) => new Error(ErrorType.BadRequest, $"{field} can't be empty");
    public static Error NotFound(string id) => new Error(ErrorType.NotFound, $"entity with id= {id} was not found");
    public static Error InvalidEmail(string v) => new Error(ErrorType.BadRequest, $"{v} is not valid email");
    public static Error NotUnique(string idnp) => new Error(ErrorType.Dublicate, $"IdentificationNumber: {idnp} is not unique");
}
public class CadastralWorkErrors : CommonErrors
{

}

public class RequestErrors : CommonErrors
{
    public static Error InvalidDateAvailableFrom() => new Error(ErrorType.BadRequest, "Invalid date available from");
    public static Error PerformerRightViolation(string personId) => new Error(ErrorType.BadRequest, $"this person: {personId} has no rights for performer");
    public static Error ResponsibleRightViolation(string personId) => new Error(ErrorType.BadRequest, $"this person: {personId} has no rights for responsible");
    public static Error InvalidDeadline() => new Error(ErrorType.BadRequest, "Invalid deadline");
    public static Error RequestStateError(string msg) => new Error(ErrorType.BadRequest, msg);
}