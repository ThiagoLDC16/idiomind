namespace Idiomind.Infrastructure.Common.Persistence.ValueGenerators;

public sealed class GuidV7Generator : ValueGenerator<Guid>
{
    public override bool GeneratesTemporaryValues => false;

    public override Guid Next(EntityEntry entry)
        => Guid.CreateVersion7();
}