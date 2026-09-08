namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SimulationMessageEntityTypeConfiguration : IEntityTypeConfiguration<SimulationMessage>
{
    public void Configure(EntityTypeBuilder<SimulationMessage> builder)
    {
        builder.ToTable("simulation_messages");

        builder.ConfigureCommonEntities();

        builder.Property(sm => sm.Sender)
            .IsRequired();

        builder.Property(sm => sm.Content)
            .IsRequired();

        builder.Property(sm => sm.TranslatedContent)
            .IsRequired(false);

        builder.OwnsOne(sm => sm.Feedback, f =>
        {
            f.Property(fb => fb.Classification)
                .IsRequired();
            
            f.Property(fb => fb.Explanation)
                .IsRequired(false);
            
            f.Property(fb => fb.Correction)
                .IsRequired(false);
        });

        builder.HasOne<Simulation>()
            .WithMany(s => s.Messages)
            .HasForeignKey(sm => sm.SimulationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
