namespace Idiomind.Infrastructure.Identity.Persistence.Configurations;

public sealed class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        builder.ConfigureCommonEntities();

        builder
            .Property(rt => rt.Token)
            .IsRequired();

        builder
            .Property(rt => rt.UserId)
            .IsRequired();

        builder.HasIndex(rt => rt.Token)
            .IsUnique();

        builder
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}