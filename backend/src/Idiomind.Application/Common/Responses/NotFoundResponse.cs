namespace Idiomind.Application.Common.Responses;

public sealed record NotFoundResponse<T> : Response<T>
{
    public NotFoundResponse(string message)
        : base(ResponseStatus.NotFound, message)
    {
    }
}

public sealed record NotFoundResponse : Response
{
    public NotFoundResponse(string message)
        : base(ResponseStatus.NotFound, message)
    {
    }
}