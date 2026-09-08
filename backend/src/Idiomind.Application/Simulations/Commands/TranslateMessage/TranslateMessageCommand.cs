namespace Idiomind.Application.Simulations.Commands.TranslateMessage;

public sealed record TranslateMessageCommand(Guid SimulationId, Guid MessageId)
    : IRequest<Response<TranslateMessageDto>>;
