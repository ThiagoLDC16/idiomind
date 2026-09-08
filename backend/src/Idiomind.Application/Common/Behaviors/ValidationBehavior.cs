namespace Idiomind.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator is null)
            return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
            return await next();
        
        var responseType = typeof(TResponse);

        var unprocessableResponseType = responseType.IsGenericType
            ? typeof(UnprocessableResponse<>).MakeGenericType(responseType.GetGenericArguments()[0])
            : typeof(UnprocessableResponse);

        var unprocessableResponse = Activator.CreateInstance(unprocessableResponseType, validationResult);
        if (unprocessableResponse is TResponse result)
            return await Task.FromResult(result);

        return await next();
    }
}