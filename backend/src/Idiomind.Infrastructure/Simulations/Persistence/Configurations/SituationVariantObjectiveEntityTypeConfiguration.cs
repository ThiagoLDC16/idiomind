namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationVariantObjectiveEntityTypeConfiguration : IEntityTypeConfiguration<SituationVariantObjective>
{
    public void Configure(EntityTypeBuilder<SituationVariantObjective> builder)
    {
        builder.ToTable("situation_variant_objectives");

        builder.ConfigureCommonEntities();

        builder.HasOne<SituationVariant>()
            .WithMany(sv => sv.Objectives)
            .HasForeignKey(svo => svo.SituationVariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
