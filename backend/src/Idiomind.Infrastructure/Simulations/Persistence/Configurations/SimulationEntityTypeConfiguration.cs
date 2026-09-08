namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SimulationEntityTypeConfiguration : IEntityTypeConfiguration<Simulation>
{
    public void Configure(EntityTypeBuilder<Simulation> builder)
    {
        builder.ToTable("simulations");

        builder.ConfigureCommonEntities();

        builder.Property(s => s.Status)
            .IsRequired();

        builder.Property(s => s.CompletedAt)
            .IsRequired(false);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Situation>()
            .WithMany()
            .HasForeignKey(s => s.SituationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Variant)
            .WithMany()
            .HasForeignKey(s => s.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Messages)
            .WithOne()
            .HasForeignKey(sm => sm.SimulationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsMany(s => s.CompletedObjectives, co =>
        {
            co.ToTable("simulation_completed_objectives");
            
            co.WithOwner().HasForeignKey(x => x.SimulationId);
            
            co.HasKey(x => new { x.SimulationId, x.ObjectiveId });

            co.Property(x => x.CompletedAt)
                .IsRequired(false);
            
            co.HasOne<SituationVariantObjective>()
                .WithMany()
                .HasForeignKey(x => x.ObjectiveId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
