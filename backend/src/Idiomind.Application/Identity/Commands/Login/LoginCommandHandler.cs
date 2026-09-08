namespace Idiomind.Application.Identity.Commands.Login;

public sealed class LoginCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    AuthService authService
) : IRequestHandler<LoginCommand, Response<AuthDto>>
{
    public async Task<Response<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null)
            return new UnauthorizedResponse<AuthDto>(IdentityMessages.UserNotFound);

        var isPasswordMatch = passwordHasher.Verify(
            plainText: request.Password,
            hash: user.PasswordHash
        );

        if (!isPasswordMatch)
            return new UnauthorizedResponse<AuthDto>(IdentityMessages.WrongPassword);

        var claims = authService.CreateUserTokenClaims(user);

        var accessToken = tokenService.GenerateAccessToken(claims);

        var refreshToken = new RefreshToken(
            userId: user.Id,
            token: tokenService.GenerateRefreshToken()
        );

        await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new OkResponse<AuthDto>(
            data: new AuthDto(
                AccessToken: accessToken,
                RefreshToken: refreshToken.Token
            )
        );
    }
}