using Transport.Api.Api;
using Transport.Api.Models;

namespace Transport.Api.Applications
{
    public class VehicleApplication : IVehicleApplication
    {
        private readonly VehicleService _vehicleService;

        public VehicleApplication(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            try
            {
                var transportInfo = await _vehicleService.GetTransportInfo();

                if (transportInfo == null)
                    return null;

                return transportInfo.listings;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(int numberOfPassengers)
        {
            try
            {
                var vehicles = await GetVehiclesAsync();
                var result = new List<Vehicle>();

                if (vehicles != null && numberOfPassengers > 0)
                {
                    result = vehicles.Where(v => v.VehicleType.MaxPassengers > 1).Take(numberOfPassengers).ToList();
                }

                return result;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
