namespace Idiomind.Application.Common.Responses;

public sealed record ConflictResponse<T> : Response<T>
{
    public ConflictResponse(string message)
        : base(ResponseStatus.Conflict, message) { }
}

public sealed record ConflictResponse : Response
{
    public ConflictResponse(string message)
        : base(ResponseStatus.Conflict, message) { }
}
