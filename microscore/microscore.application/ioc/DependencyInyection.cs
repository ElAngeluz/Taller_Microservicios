using microscore.application.interfaces.services;
using microscore.application.services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace microscore.application.ioc
{
    public static class DependencyInyection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IClientsServices, ClientsServices>();
            services.AddScoped<IAccountServices, AccountServices>();
            return services;
        }
    }
}
