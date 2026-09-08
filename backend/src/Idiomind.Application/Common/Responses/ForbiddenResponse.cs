namespace Idiomind.Application.Common.Responses;

public sealed record ForbiddenResponse<T> : Response<T>
{
    public ForbiddenResponse(string message)
        : base(ResponseStatus.Forbidden, message)
    {
    }
}

public sealed record ForbiddenResponse : Response
{
    public ForbiddenResponse(string message)
        : base(ResponseStatus.Forbidden, message)
    {
    }
}