namespace Idiomind.Application.Common.Responses;

public sealed record OkResponse<T> : Response<T>
{
    public OkResponse() : base(ResponseStatus.Ok)
    {
    }

    public OkResponse(T data) : base(ResponseStatus.Ok, data)
    {
    }
}

public sealed record OkResponse() : Response(ResponseStatus.Ok);