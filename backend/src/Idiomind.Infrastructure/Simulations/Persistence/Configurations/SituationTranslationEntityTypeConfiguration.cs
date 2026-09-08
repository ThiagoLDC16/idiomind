namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationTranslationEntityTypeConfiguration : IEntityTypeConfiguration<SituationTranslation>
{
    public void Configure(EntityTypeBuilder<SituationTranslation> builder)
    {
        builder.ToTable("situation_translations");

        builder.ConfigureCommonEntities();

        builder.Property(st => st.Name)
            .IsRequired();

        builder.Property(st => st.Description)
            .IsRequired();
    }
}
