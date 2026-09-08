namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class SituationEntityTypeConfiguration : IEntityTypeConfiguration<Situation>
{
    public void Configure(EntityTypeBuilder<Situation> builder)
    {
        builder.ToTable("situations");

        builder.ConfigureCommonEntities();

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
