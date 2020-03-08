using Domain.Interfaces.Services;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class BootstrapConfiguration
    {
        public static IServiceCollection Bootstrap(this IServiceCollection services)
        {
            services.AddScoped<ITollService, TollService>();
            return services;
        }
    }
}
