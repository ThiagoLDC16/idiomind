namespace Idiomind.Application.Identity.Queries.GetLoggedUser;

public sealed class GetLoggedUserQueryHandler(
    IAppDbContext context,
    IRequestContext requestContext
) : IRequestHandler<GetLoggedUserQuery, Response<GetLoggedUserDto>>
{
    public async Task<Response<GetLoggedUserDto>> Handle(
        GetLoggedUserQuery request,
        CancellationToken cancellationToken
    )
    {
        if (requestContext.UserId is null)
            return new UnauthorizedResponse<GetLoggedUserDto>(IdentityMessages.UserNotFound);

        var userId = requestContext.UserId.Value;

        var nativeLanguage = await context.UserProfiles
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(p => p.NativeLanguage)
            .FirstOrDefaultAsync(cancellationToken);

        var user = await context.Users
            .AsNoTracking()
            .Select(u => new GetLoggedUserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                HasProfile = nativeLanguage != null,
                NativeLanguage = nativeLanguage
            })
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user is null)
            return new UnauthorizedResponse<GetLoggedUserDto>(IdentityMessages.UserNotFound);

        return new OkResponse<GetLoggedUserDto>(user);
    }
}
