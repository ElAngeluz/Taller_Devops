using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Reflection;

namespace taller.devops.infraestructura.ioc
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                  .ReadFrom
                  .Configuration(configuration).CreateLogger();

            //Se agrega uso de Logs
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

            // Se agrega Telemetria
            _ = int.TryParse(configuration["Jaeger:Telemetry:Port"], out int portNumber);


            services.AddOpenTelemetry().WithTracing((tracerProviderBuilder) =>
            {
                tracerProviderBuilder
                .AddSource(configuration["Serilog:Properties:Application"])
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: configuration["Serilog:Properties:Application"]))
                .AddHttpClientInstrumentation()
                .AddJaegerExporter(opts =>
                {
                    opts.AgentHost = configuration["Jaeger:Telemetry:Host"];
                    opts.AgentPort = portNumber;
                });
            }).WithMetrics();

            // Se agrega Librerias de Mapeos
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
