using Microsoft.Extensions.DependencyInjection;
using taller.devops.application.interfaces.services;
using taller.devops.application.repositories.notification;

namespace taller.devops.application.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ISendNotification, SendNotificationRepository>();
            return services;
        }
    }
}
