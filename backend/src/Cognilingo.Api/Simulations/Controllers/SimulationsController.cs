namespace Cognilingo.Api.Simulations.Controllers;

[Route("api/simulations")]
[Authorize]
public class SimulationsController(IMediator _mediator) : BaseController
{
    [HttpGet("categories")]
    public async Task<IActionResult> ListCategories([FromQuery] ListCategoriesQuery query)
        => MapResponse(await _mediator.Send(query));

    [HttpGet("categories/{categoryId:guid}")]
    public async Task<IActionResult> ListSituations(Guid categoryId, [FromQuery] string languageCode)
        => MapResponse(await _mediator.Send(new ListSituationsQuery(categoryId, languageCode)));

    [HttpGet("situations/{situationId:guid}")]
    public async Task<IActionResult> ListSituationVariants(Guid situationId, [FromQuery] string languageCode)
        => MapResponse(await _mediator.Send(new ListSituationVariantsQuery(situationId, languageCode)));

    [HttpPost]
    public async Task<IActionResult> StartSimulation([FromBody] StartSimulationCommand command)
        => MapResponse(await _mediator.Send(command));

    [HttpGet("{id:guid}/messages")]
    public async Task<IActionResult> ListSimulationMessages(Guid id, [FromQuery] string languageCode)
        => MapResponse(await _mediator.Send(new ListSimulationMessagesQuery(id, languageCode)));

    [HttpPost("{id:guid}/messages")]
    public async Task<IActionResult> SendMessage(Guid id, [FromBody] SendMessagePayload payload)
        => MapResponse(await _mediator.Send(payload.AsCommand(id)));

    [HttpPost("{id:guid}/messages/{messageId:guid}/translate")]
    public async Task<IActionResult> TranslateMessage(Guid id, Guid messageId)
        => MapResponse(await _mediator.Send(new TranslateMessageCommand(id, messageId)));

    [HttpPost("{id:guid}/finish")]
    public async Task<IActionResult> FinishSimulation(Guid id)
        => MapResponse(await _mediator.Send(new FinishSimulationCommand(id)));
}
