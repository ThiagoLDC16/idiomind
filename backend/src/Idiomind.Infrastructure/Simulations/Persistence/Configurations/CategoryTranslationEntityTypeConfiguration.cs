namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class CategoryTranslationEntityTypeConfiguration : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {
        builder.ToTable("category_translations");
        
        builder.ConfigureCommonEntities();
        
        builder.Property(ct => ct.Name)
            .IsRequired();
    }
}