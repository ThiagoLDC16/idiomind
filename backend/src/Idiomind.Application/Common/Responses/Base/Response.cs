namespace Idiomind.Application.Common.Responses.Base;

public abstract record Response
{
    public ResponseStatus Status { get; init; } = ResponseStatus.Undefined;
    public ISet<string> Messages { get; init; } = new HashSet<string>();

    protected Response(ResponseStatus status) => Status = status;

    protected Response(ResponseStatus status, string message) : this(status)
        => Messages.Add(message);

    protected Response(ResponseStatus status, IEnumerable<string> messages) : this(status)
        => Messages.UnionWith(messages);
}

public abstract record Response<T> : Response
{
    public T? Data { get; init; }

    protected Response(ResponseStatus status) : base(status)
    {
    }

    protected Response(ResponseStatus status, T data) : base(status)
        => Data = data;

    protected Response(ResponseStatus status, string message) : base(status, message)
    {
    }

    protected Response(ResponseStatus status, IEnumerable<string> messages) : base(status, messages)
    {
    }
}