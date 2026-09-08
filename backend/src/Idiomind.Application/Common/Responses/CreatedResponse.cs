namespace Idiomind.Application.Common.Responses;

public sealed record CreatedResponse<T> : Response<T>
{
    public CreatedResponse()
        : base(ResponseStatus.Created)
    {
    }

    public CreatedResponse(T data)
        : base(ResponseStatus.Created, data)
    {
    }
}

public sealed record CreatedResponse() : Response(ResponseStatus.Created);