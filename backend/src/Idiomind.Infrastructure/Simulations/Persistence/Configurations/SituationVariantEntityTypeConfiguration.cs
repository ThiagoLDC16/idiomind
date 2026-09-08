namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationVariantEntityTypeConfiguration : IEntityTypeConfiguration<SituationVariant>
{
    public void Configure(EntityTypeBuilder<SituationVariant> builder)
    {
        builder.ToTable("situation_variants");

        builder.ConfigureCommonEntities();

        builder.Property(sv => sv.LearningLanguage)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(sv => sv.PromptInstructions)
            .IsRequired();

        builder.HasOne<Situation>()
            .WithMany()
            .HasForeignKey(sv => sv.SituationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
