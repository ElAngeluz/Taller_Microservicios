using microscore.application.interfaces.repositories;
using microscore.infrastructure.data.context;
using microscore.infrastructure.data.repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            var builderConnection = new SqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<MicrosContext>(options =>
            {
                options.UseSqlServer(builderConnection.ConnectionString);
            },
            ServiceLifetime.Transient
            );

            services.AddDbContextFactory<MicrosContext>(options =>
            {
                options.UseSqlServer(builderConnection.ConnectionString);
            }, ServiceLifetime.Transient);

            var options = new DbContextOptionsBuilder<MicrosContext>()
            .UseSqlServer(builderConnection.ConnectionString)
            .Options;

            using (var context = new MicrosContext(options))
            {
                var migrator = context.Database.GetService<IMigrator>();
                var generator = context.Database.GetService<IMigrationsSqlGenerator>();
                var sql = migrator.GenerateScript();

                File.WriteAllText("../../BaseDatos.sql", sql);
            }

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
