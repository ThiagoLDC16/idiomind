namespace Idiomind.Application.Simulations.Commands.StartSimulation;

public sealed record StartSimulationCommand(
    Guid VariantId
) : IRequest<Response<Guid>>;

public sealed class StartSimulationCommandValidator : AbstractValidator<StartSimulationCommand>
{
    public StartSimulationCommandValidator()
    {
        RuleFor(x => x.VariantId).NotEmpty();
    }
}
