namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationVariantObjectiveTranslationEntityTypeConfiguration : IEntityTypeConfiguration<SituationVariantObjectiveTranslation>
{
    public void Configure(EntityTypeBuilder<SituationVariantObjectiveTranslation> builder)
    {
        builder.ToTable("situation_variant_objective_translations");

        builder.ConfigureCommonEntities();

        builder.Property(svot => svot.Name)
            .IsRequired();
    }
}
