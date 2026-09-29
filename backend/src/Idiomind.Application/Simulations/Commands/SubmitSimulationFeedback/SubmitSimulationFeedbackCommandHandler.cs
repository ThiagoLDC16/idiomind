namespace Idiomind.Application.Simulations.Commands.SubmitSimulationFeedback;

public sealed class SubmitSimulationFeedbackCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext
) : IRequestHandler<SubmitSimulationFeedbackCommand, Response>
{
    public async Task<Response> Handle(
        SubmitSimulationFeedbackCommand request,
        CancellationToken cancellationToken
    )
    {
        var userId = requestContext.UserId!.Value;
        var simulation = await context.Simulations
            .FirstOrDefaultAsync(
                simulation => simulation.Id == request.SimulationId && simulation.UserId == userId,
                cancellationToken
            );

        if (simulation is null)
            return new NotFoundResponse(SimulationMessages.SimulationNotFound);

        if (simulation.Status != SimulationStatus.Completed)
            return new UnprocessableResponse(SimulationMessages.SimulationNotCompleted);

        var alreadySubmitted = await context.SimulationFeedbacks
            .AnyAsync(feedback => feedback.SimulationId == request.SimulationId, cancellationToken);

        if (alreadySubmitted)
            return new ConflictResponse(SimulationMessages.SimulationFeedbackAlreadySubmitted);

        var feedback = new SimulationFeedback(
            simulationId: simulation.Id,
            userId: userId,
            experienceRating: request.ExperienceRating,
            preparedness: request.Preparedness,
            improvement: request.Improvement,
            skipped: request.Skipped
        );

        context.SimulationFeedbacks.Add(feedback);
        await context.SaveChangesAsync(cancellationToken);

        return new NoContentResponse();
    }
}
