using Microsoft.AspNetCore.Mvc;

namespace microscore.api.Extentions
{
    public static class ServiceExtentions
    {

        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            return services;

        }

    }
}
