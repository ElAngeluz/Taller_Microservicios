using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace microscore.infrastructure.security
{
    public static class TokenValidationHandler
    {
        public static IServiceCollection SetupAuthenticationServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = builder.Configuration["AzureAdAudience"];
                    options.Authority = builder.Configuration["AzureAdInstance"] + builder.Configuration["AzureAdTenantId"];
                    options.TokenValidationParameters.ValidAudiences = new string?[] { options.Audience, $"api://{options.Audience}" };

                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidateLifetime = true;
                    options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                    options.Events = new JwtBearerEvents();
                });
            return services;
        }
    }
}
