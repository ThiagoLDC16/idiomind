namespace Idiomind.Application.Common.Responses;

public sealed record UnprocessableResponse<T> : Response<T>
{
    public UnprocessableResponse(string message)
        : base(ResponseStatus.Unprocessable, message) { }
    public UnprocessableResponse(IEnumerable<string> messages)
        : base(ResponseStatus.Unprocessable, messages) { }
    public UnprocessableResponse(ValidationResult validate)
        : base(ResponseStatus.Unprocessable, validate.Errors.Select(x => x.ErrorMessage).ToList()) { }
}

public sealed record UnprocessableResponse : Response
{
    public UnprocessableResponse(string message)
        : base(ResponseStatus.Unprocessable, message) { }
    public UnprocessableResponse(IEnumerable<string> messages)
        : base(ResponseStatus.Unprocessable, messages) { }
    public UnprocessableResponse(ValidationResult validate)
        : base(ResponseStatus.Unprocessable, validate.Errors.Select(x => x.ErrorMessage).ToList()) { }
}