using Microsoft.AspNetCore.Mvc;

namespace taller.devops.api.Extensions
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

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;

        }

    }
}
