using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Services;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class BootstrapConfiguration
    {
        public static IServiceCollection Bootstrap(this IServiceCollection services)
        {
            services.AddScoped<ITollService, TollService>();
            services.AddScoped<ITollFeeRepository, TollFeeRepository>();
            return services;
        }
    }
}
