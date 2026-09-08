namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        
        builder.ConfigureCommonEntities();

        builder.Property(c => c.Icon)
            .IsRequired();
    }
}