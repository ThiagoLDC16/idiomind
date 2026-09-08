namespace Idiomind.Application.Simulations.Commands.SendMessage;

public sealed class SendMessageCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext,
    IChatCompletionService chatCompletionService,
    IMessageFeedbackService messageFeedbackService
) : IRequestHandler<SendMessageCommand, Response<List<SimulationMessageDto>>>
{
    public async Task<Response<List<SimulationMessageDto>>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var simulation = await context.Simulations
            .Include(s => s.Messages)
            .Include(s => s.Variant)
            .FirstOrDefaultAsync(
                s => s.Id == request.SimulationId && s.UserId == requestContext.UserId.Value,
                cancellationToken
            );

        if (simulation is null)
            return new NotFoundResponse<List<SimulationMessageDto>>(SimulationMessages.SimulationNotFound);

        var history = simulation.Messages
            .Select(m => new Message(m.Sender, m.Content))
            .ToList();

        var userMessage = simulation.AddUserMessage(request.Content);

        var chatTask = chatCompletionService.GenerateResponseAsync(
            new ChatRequest(
                History: history,
                SystemPrompt: simulation.Variant.PromptInstructions,
                UserMessage: request.Content
            ),
            cancellationToken
        );

        var feedbackTask = messageFeedbackService.GenerateFeedbackAsync(
            new FeedbackRequest(
                History: history,
                LanguageCode: simulation.Variant.LearningLanguage,
                UserMessage: request.Content
            ),
            cancellationToken
        );

        await Task.WhenAll(chatTask, feedbackTask);

        var chatResponse = await chatTask;
        var feedbackResponse = await feedbackTask;

        userMessage.SetFeedback(
            new SimulationMessageFeedback(
                classification: feedbackResponse.Classification,
                explanation: feedbackResponse.Explanation,
                correction: feedbackResponse.Correction
            )
        );

        var assistantMessage = simulation.AddAssistantMessage(chatResponse.AssistantResponse);

        await context.SaveChangesAsync(cancellationToken);

        var result = new List<SimulationMessageDto>
        {
            SimulationMessageDto.FromDomain(userMessage),
            SimulationMessageDto.FromDomain(assistantMessage)
        };

        return new OkResponse<List<SimulationMessageDto>>(result);
    }
}