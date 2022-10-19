using System.Text.Json;

namespace BaseProject.DependencyInjection
{
    public static class DomainCollectionExtension
    {
        public static IServiceCollection AddDomains(this IServiceCollection services)
        {
            
            services.AddControllersWithViews();            

            //services.AddScoped<IUserRepository, UserRepository>();  

            return services;
        }
    }
}
