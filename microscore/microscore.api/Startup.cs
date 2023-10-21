using microscore.api.Extentions;
using microscore.application.ioc;
using microscore.infrastructure.extentions;
using microscore.infrastructure.ioc;
using microscore.infrastructure.security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

namespace microscore.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {




            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                var version = Configuration["OpenApi:info:version"];
                var title = Configuration["OpenApi:info:title"];
                var description = Configuration["OpenApi:info:description"];
                var termsOfService = new Uri(Configuration["OpenApi:info:termsOfService"]);
                var contact = new OpenApiContact
                {
                    Name = Configuration["OpenApi:info:contact:name"],
                    Url = new Uri(Configuration["OpenApi:info:contact:url"]),
                    Email = Configuration["OpenApi:info:contact:email"]
                };
                var license = new OpenApiLicense
                {
                    Name = Configuration["OpenApi:info:License:name"],
                    Url = new Uri(Configuration["OpenApi:info:License:url"])
                };
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = title,
                    Description = description,
                    TermsOfService = termsOfService,
                    Contact = contact,
                    License = license
                });
                options.SwaggerDoc(Configuration["OpenApi:info:version"], new OpenApiInfo
                {
                    Version = Configuration["OpenApi:info:version"],
                    Title = title,
                    Description = description,
                    TermsOfService = termsOfService,
                    Contact = contact,
                    License = license
                });
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Put **_ONLY_** your JWT Bearer **_token_** on textbox below! \r\n\r\n\r\n Example: \"Value: **12345abcdef**\"",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                options.OperationFilter<RequiredHeaderParameter>();
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

            });


            //Dependencias propias de Servicio
            services.RegisterDependencies();
            services.AddInfrastructure(Configuration);
            services.AddApplication();


            _ = int.TryParse(Configuration["Jaeger:Telemetry:Port"], out int portNumber);

            //services.AddControllers();

            services.AddOpenTelemetryTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                .AddSource(Configuration["Serilog:Properties:Application"])

                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: Configuration["Serilog:Properties:Application"]))
                .AddHttpClientInstrumentation()

                .AddAspNetCoreInstrumentation(options =>
                {

                    options.Enrich = (activity, eventName, rawObject) =>
                    {

                        string? traceid = string.Empty;

                        if (rawObject is HttpRequest httpRequest)
                        {
                            traceid = httpRequest.HttpContext?.TraceIdentifier;
                            activity.SetTag("Log-Traceid", traceid);
                        }

                    };
                })
                .AddSqlClientInstrumentation(options =>
                {
                    options.EnableConnectionLevelAttributes = true;
                    options.SetDbStatementForStoredProcedure = true;
                    options.SetDbStatementForText = true;
                    options.RecordException = true;
                    options.Enrich = (activity, x, y) => activity.SetTag("db.type", "sql");
                })
                .AddJaegerExporter(opts =>
                {
                    opts.AgentHost = Configuration["Jaeger:Telemetry:Host"];
                    opts.AgentPort = portNumber;
                });


            });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                    c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                });
            }

            app.ConfigureMetricServer();
            app.ConfigureExceptionHandler();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/health/readiness", async context =>
                {
                    await context.Response.WriteAsync("Ok");
                });

                endpoints.MapGet("/health/liveness", async context =>
                {
                    await context.Response.WriteAsync("Ok");
                });

                endpoints.MapControllers();
            });


        }
    }
}
