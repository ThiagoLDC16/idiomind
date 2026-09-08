namespace Idiomind.Application.Common.Responses;

public sealed record NoContentResponse<T>() : Response<T>(ResponseStatus.NoContent);

public sealed record NoContentResponse() : Response(ResponseStatus.NoContent);