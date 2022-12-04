using Transport.Api.Api;
using Transport.Api.Applications;

namespace Transport.Api.Utilities
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection InitialApplications(this IServiceCollection services)
        {
            services.AddScoped<IVehicleApplication, VehicleApplication>();

            return services;
        }

        public static IServiceCollection AddCustomHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<VehicleService>();

            services.AddHttpClient<LocationService>();

            return services;
        }
    }
}
