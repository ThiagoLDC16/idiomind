namespace Idiomind.Application.Identity.Commands.RefreshTokens;

public sealed class RefreshTokenCommandHandler(
    IAppDbContext context,
    ITokenService tokenService,
    AuthService authService
) : IRequestHandler<RefreshTokenCommand, Response<AuthDto>>
{
    public async Task<Response<AuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (refreshToken is null)
            return new UnauthorizedResponse<AuthDto>(IdentityMessages.InvalidRefreshToken);

        if (refreshToken.IsExpired())
        {
            context.RefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
            return new UnauthorizedResponse<AuthDto>(IdentityMessages.ExpiredRefreshToken);
        }

        var claims = authService.CreateUserTokenClaims(refreshToken.User);
        var accessToken = tokenService.GenerateAccessToken(claims);

        refreshToken.UpdateToken(
            tokenService.GenerateRefreshToken()
        );

        await context.SaveChangesAsync(cancellationToken);

        return new OkResponse<AuthDto>(
            data: new AuthDto(
                AccessToken: accessToken,
                RefreshToken: refreshToken.Token
            )
        );
    }
}