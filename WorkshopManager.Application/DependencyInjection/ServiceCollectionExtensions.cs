using Microsoft.Extensions.DependencyInjection;
using WorkshopManager.Application.Interfaces;
using WorkshopManager.Application.Services;

namespace WorkshopManager.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<ICitaService, CitaService>();

            return services;
        }
    }
}
