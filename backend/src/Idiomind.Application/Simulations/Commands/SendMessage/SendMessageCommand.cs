namespace Idiomind.Application.Simulations.Commands.SendMessage;

public sealed record SendMessageCommand(
    Guid SimulationId,
    string Content
) : IRequest<Response<List<SimulationMessageDto>>>;

public sealed class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.SimulationId)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(2000);
    }
}