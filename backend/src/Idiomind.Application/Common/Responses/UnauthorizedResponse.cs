namespace Idiomind.Application.Common.Responses;

public sealed record UnauthorizedResponse<T> : Response<T>
{
    public UnauthorizedResponse(string message)
        : base(ResponseStatus.Unauthorized, message)
    {
    }
}

public sealed record UnauthorizedResponse : Response
{
    public UnauthorizedResponse(string message)
        : base(ResponseStatus.Unauthorized, message)
    {
    }
}