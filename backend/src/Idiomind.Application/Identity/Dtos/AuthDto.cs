namespace Idiomind.Application.Identity.Dtos;

public sealed record AuthDto(
    string AccessToken,
    string RefreshToken
);