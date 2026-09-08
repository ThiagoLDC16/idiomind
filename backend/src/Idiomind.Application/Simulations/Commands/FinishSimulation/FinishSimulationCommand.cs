namespace Idiomind.Application.Simulations.Commands.FinishSimulation;

public sealed record FinishSimulationCommand(
    Guid SimulationId
) : IRequest<Response>;

public sealed class FinishSimulationCommandValidator : AbstractValidator<FinishSimulationCommand>
{
    public FinishSimulationCommandValidator()
    {
        RuleFor(x => x.SimulationId).NotEmpty();
    }
}
