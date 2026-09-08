namespace Idiomind.Application.Identity.Commands.Register;

public sealed class RegisterCommandHandler(
    IAppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    AuthService authService
) : IRequestHandler<RegisterCommand, Response<AuthDto>>
{
    public async Task<Response<AuthDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var hasUser = await context.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (hasUser)
            return new UnprocessableResponse<AuthDto>(IdentityMessages.EmailAlreadyUsed);

        var user = new User(
            name: request.Name,
            email: request.Email,
            passwordHash: passwordHasher.Hash(request.Password)
        );

        await context.Users.AddAsync(user, cancellationToken);

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