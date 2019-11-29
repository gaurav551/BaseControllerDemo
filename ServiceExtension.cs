using ClientNotifications.ServiceExtensions;
using Microsoft.Extensions.DependencyInjection;
using RepositoryDemo.Repository;

namespace RepositoryDemo
{
    //extension method
    public static class ServiceExtension
    {
        public static void CustomService(this IServiceCollection services){
            services.AddToastNotification();
            services.AddTransient<IStudentRepository,StudentRepository>();
        }
    }
}