namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddPersistence(services, configuration);
        AddIdentityInfrastructure(services);
        AddAiServices(services, configuration);

        return services;
    }

    private static void AddAiServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenAiChatOptions>(configuration.GetSection(OpenAiChatOptions.SectionName));
        services.Configure<OpenAiFeedbackOptions>(configuration.GetSection(OpenAiFeedbackOptions.SectionName));
        services.Configure<OpenAiTranslationOptions>(configuration.GetSection(OpenAiTranslationOptions.SectionName));

        services.AddScoped<IChatCompletionService, OpenAiChatCompletionService>();
        services.AddScoped<IMessageFeedbackService, OpenAiMessageFeedbackService>();
        services.AddScoped<IMessageTranslationService, OpenAiMessageTranslationService>();
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<UpdateAuditFieldsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var auditInterceptor = sp.GetRequiredService<UpdateAuditFieldsInterceptor>();

            options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(auditInterceptor);
        });

        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }

    private static void AddIdentityInfrastructure(
        IServiceCollection services
    )
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IRequestContext, RequestContext>();

        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, MD5PasswordHasher>();
    }
}