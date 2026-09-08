namespace Idiomind.Infrastructure.Common.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ConfigureCommonEntities<TEntity>(
        this EntityTypeBuilder<TEntity> builder
    ) where TEntity : class
    {
        TryConfigureBaseEntity(builder);

        TryConfigureTranslationBase(builder);
        TryConfigureTranslations(builder);
        return builder;
    }

    private static void TryConfigureBaseEntity(EntityTypeBuilder builder)
    {
        if (!builder.Metadata.ClrType.IsAssignableTo(typeof(BaseEntity)))
            return;

        builder
            .HasKey(nameof(BaseEntity.Id));

        builder
            .Property(nameof(BaseEntity.Id))
            .HasValueGenerator<GuidV7Generator>();

        builder
            .Property(nameof(BaseEntity.CreatedAt))
            .IsRequired();

        builder
            .Property(nameof(BaseEntity.UpdatedAt))
            .IsRequired(false);
    }

    private static void TryConfigureTranslationBase(
        EntityTypeBuilder builder
    )
    {
        if (!builder.Metadata.ClrType.IsAssignableTo(typeof(TranslationBase)))
            return;

        builder.HasKey(nameof(TranslationBase.EntityId), nameof(TranslationBase.LanguageCode));

        builder.Property(nameof(TranslationBase.LanguageCode))
            .IsRequired()
            .HasMaxLength(10);
    }


    private static void TryConfigureTranslations(
        EntityTypeBuilder builder
    )
    {
        var translationType = builder.Metadata.ClrType
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITranslatable<>))
            ?.GetGenericArguments()[0];

        if (translationType is null)
            return;

        builder
            .HasMany(translationType, nameof(ITranslatable<TranslationBase>.Translations))
            .WithOne()
            .HasForeignKey(nameof(TranslationBase.EntityId))
            .OnDelete(DeleteBehavior.Cascade);
    }
}