namespace Idiomind.Application.Identity.Queries.GetLoggedUser;

public sealed record GetLoggedUserQuery() : IRequest<Response<GetLoggedUserDto>>;
