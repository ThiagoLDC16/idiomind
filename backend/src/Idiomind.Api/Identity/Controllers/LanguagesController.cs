namespace Idiomind.Api.Identity.Controllers;

[Authorize]
[Route("api/identity/languages")]
public class LanguagesController(IMediator _mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetLanguages()
        => MapResponse(await _mediator.Send(new GetLanguagesQuery()));
}
