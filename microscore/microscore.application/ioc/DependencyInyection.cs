using microscore.application.interfaces.services;
using microscore.application.services;
using Microsoft.Extensions.DependencyInjection;

namespace microscore.application.ioc
{
    public static class DependencyInyection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEjemploRepository, EjemploRepository>();
            return services;
        }
    }
}
