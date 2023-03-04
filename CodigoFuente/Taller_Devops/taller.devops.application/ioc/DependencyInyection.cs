using Microsoft.Extensions.DependencyInjection;

namespace taller.devops.application.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IQualtricsRepository, QualtricsRepository>();
            return services;
        }
    }
}
