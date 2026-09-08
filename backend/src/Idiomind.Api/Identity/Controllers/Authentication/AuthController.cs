namespace Idiomind.Api.Identity.Controllers.Authentication;

[Route("api/auth")]
public class AuthController(IMediator _mediator) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
        => MapResponse(await _mediator.Send(command));

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        => MapResponse(await _mediator.Send(command));

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        => MapResponse(await _mediator.Send(command));

    [Authorize]
    [HttpGet("logged-user")]
    public async Task<IActionResult> GetLoggedUser()
        => MapResponse(await _mediator.Send(new GetLoggedUserQuery()));

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        => MapResponse(await _mediator.Send(command));
}