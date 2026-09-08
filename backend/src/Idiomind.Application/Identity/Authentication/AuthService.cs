namespace Idiomind.Application.Identity.Authentication;

public class AuthService
{
    public IEnumerable<Claim> CreateUserTokenClaims(User user)
        =>
        [
            new Claim(AppClaimTypes.UserId, user.Id.ToString())
        ];
}