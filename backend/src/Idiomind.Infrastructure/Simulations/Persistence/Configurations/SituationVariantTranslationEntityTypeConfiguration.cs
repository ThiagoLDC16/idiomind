namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationVariantTranslationEntityTypeConfiguration : IEntityTypeConfiguration<SituationVariantTranslation>
{
    public void Configure(EntityTypeBuilder<SituationVariantTranslation> builder)
    {
        builder.ToTable("situation_variant_translations");

        builder.ConfigureCommonEntities();

        builder.Property(svt => svt.Name)
            .IsRequired();

        builder.Property(svt => svt.UserContext)
            .IsRequired();
    }
}
