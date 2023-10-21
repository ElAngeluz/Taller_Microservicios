using microscore.application.interfaces.repositories;
using microscore.infrastructure.data.context;
using microscore.infrastructure.data.repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Reflection;

namespace microscore.infrastructure.ioc
{
    public static class DependencyInyection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Se agregan Logs ELK
            Log.Logger = new LoggerConfiguration()
                  .ReadFrom
                  .Configuration(configuration).CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Se agrega Telemetria
            _ = int.TryParse(configuration["Jaeger:Telemetry:Port"], out int portNumber);

            // Se agrega Librerias de Mapeos
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Se agregan los servicios
            services.AddScoped<IEjemploRestRepository, EjemploRestRepository>();

            var builderConnection = new SqlConnectionStringBuilder(configuration.GetConnectionString("ConexionBase"));

            builderConnection.Password = "Bg123456";

            services.AddDbContext<EjemploContext>(options =>
            {
                options.UseSqlServer(builderConnection.ConnectionString);
            },
            ServiceLifetime.Transient
            );
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
