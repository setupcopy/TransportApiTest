using Transport.Api.Models;

namespace Transport.Api.Dtos
{
    public class VehicleDto
    {
        public string Name { get; set; }
        public decimal PricePerPassenger { get; set; }
        public decimal TotalPrice { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
