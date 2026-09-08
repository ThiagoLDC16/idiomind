namespace Idiomind.Application.Simulations.Commands.FinishSimulation;

public sealed class FinishSimulationCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext
) : IRequestHandler<FinishSimulationCommand, Response>
{
    public async Task<Response> Handle(
        FinishSimulationCommand request,
        CancellationToken cancellationToken
    )
    {
        var simulation = await context.Simulations
            .FirstOrDefaultAsync(
                s => s.Id == request.SimulationId && s.UserId == requestContext.UserId.Value,
                cancellationToken
            );

        if (simulation is null)
            return new NotFoundResponse(SimulationMessages.SimulationNotFound);

        simulation.Complete();

        await context.SaveChangesAsync(cancellationToken);

        return new NoContentResponse();
    }
}
