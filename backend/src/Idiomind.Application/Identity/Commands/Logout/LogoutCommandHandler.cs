namespace Idiomind.Application.Identity.Commands.Logout;

public sealed class LogoutCommandHandler(IAppDbContext context)
    : IRequestHandler<LogoutCommand, Response>
{
    public async Task<Response> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is not null)
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        return new NoContentResponse();
    }
}
