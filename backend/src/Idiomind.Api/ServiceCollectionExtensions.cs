namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtBearerAuthentication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtAuthOptionsSection = configuration.GetSection("Authentication:Jwt");

        services.AddOptions<JwtAuthOptions>().Bind(jwtAuthOptionsSection);

        var jwtAuthOptions = jwtAuthOptionsSection.Get<JwtAuthOptions>()
                             ?? throw new InvalidOperationException("JwtAuthOptions not found in configuration");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = jwtAuthOptions.Issuer,
                    ValidAudience = jwtAuthOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtAuthOptions.SecurityKey)
                    )
                };

                options.MapInboundClaims = false;
            });

        return services;
    }
}