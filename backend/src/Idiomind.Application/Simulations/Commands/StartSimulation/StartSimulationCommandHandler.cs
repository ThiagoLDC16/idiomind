namespace Idiomind.Application.Simulations.Commands.StartSimulation;

public sealed class StartSimulationCommandHandler(
    IAppDbContext context,
    IRequestContext requestContext
) : IRequestHandler<StartSimulationCommand, Response<Guid>>
{
    public async Task<Response<Guid>> Handle(
        StartSimulationCommand request,
        CancellationToken cancellationToken
    )
    {
        var variant = await context.SituationVariants
            .AsNoTracking()
            .FirstOrDefaultAsync(sv => sv.Id == request.VariantId, cancellationToken);

        if (variant is null)
            return new NotFoundResponse<Guid>(SimulationMessages.VariantNotFound);

        var simulation = new Simulation(
            userId: requestContext.UserId.Value,
            situationId: variant.SituationId,
            variantId: variant.Id,
            initialMessage: variant.InitialMessage
        );

        context.Simulations.Add(simulation);
        await context.SaveChangesAsync(cancellationToken);

        return new OkResponse<Guid>(simulation.Id);
    }
}
