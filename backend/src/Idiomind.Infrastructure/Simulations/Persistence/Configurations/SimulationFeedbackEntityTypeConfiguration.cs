namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SimulationFeedbackEntityTypeConfiguration
    : IEntityTypeConfiguration<SimulationFeedback>
{
    public void Configure(EntityTypeBuilder<SimulationFeedback> builder)
    {
        builder.ToTable("simulation_feedback");

        builder.ConfigureCommonEntities();

        builder.Property(feedback => feedback.ExperienceRating)
            .IsRequired(false);

        builder.Property(feedback => feedback.Preparedness)
            .IsRequired(false);

        builder.Property(feedback => feedback.Improvement)
            .HasMaxLength(2000)
            .IsRequired(false);

        builder.Property(feedback => feedback.Skipped)
            .IsRequired();

        builder.HasKey(feedback => feedback.Id);

        builder.HasIndex(feedback => feedback.SimulationId)
            .IsUnique();

        builder.HasOne(feedback => feedback.Simulation)
            .WithOne()
            .HasForeignKey<SimulationFeedback>(feedback => feedback.SimulationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(feedback => feedback.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
