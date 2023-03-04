using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Extensions.Hosting;
using System.Threading.Tasks;
using OpenTelemetry.Instrumentation.Http;

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
