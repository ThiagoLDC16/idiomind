namespace Idiomind.Infrastructure.Simulations.Persistence.Configurations;

public sealed class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("user_profiles");

        builder.ConfigureCommonEntities();

        builder.Property(up => up.NativeLanguage)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(up => up.LearningLanguage)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Simulation>()
            .WithMany()
            .HasForeignKey(up => up.NextRecommendedSimulation)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
