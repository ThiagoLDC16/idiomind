namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidation(assembly);

        AddIdentity(services);

        return services;
    }

    private static void AddIdentity(IServiceCollection services)
    {
        services.AddScoped<AuthService>();
    }
}