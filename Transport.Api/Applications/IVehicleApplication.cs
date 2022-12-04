using Transport.Api.Models;

namespace Transport.Api.Applications
{
    public interface IVehicleApplication
    {
        public Task<List<Vehicle>> GetVehiclesAsync();
        public Task<List<Vehicle>> GetVehiclesAsync(int numberOfPassengers);
    }
}
