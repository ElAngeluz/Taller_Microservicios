using microscore.infrastructure.extentions;
using microscore.infrastructure.ioc;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

_ = int.TryParse(builder.Configuration["Jaeger:Telemetry:Port"], out int portNumber);

//services.AddControllers();

builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    .AddSource(builder.Configuration["Serilog:Properties:Application"])
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: builder.Configuration["Serilog:Properties:Application"]))
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
        opts.AgentHost = builder.Configuration["Jaeger:Telemetry:Host"];
        opts.AgentPort = portNumber;
    });


});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureMetricServer();
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapHealthChecks("/health/readiness", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
});

app.MapHealthChecks("/health/liveness", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
    Predicate = _ => false
});

app.MapControllers();

app.Run();
