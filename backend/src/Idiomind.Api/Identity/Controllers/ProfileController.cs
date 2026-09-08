namespace Idiomind.Api.Identity.Controllers;

[Authorize]
[Route("api/identity/profile")]
public class ProfileController(IMediator _mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileCommand command)
        => MapResponse(await _mediator.Send(command));
}
