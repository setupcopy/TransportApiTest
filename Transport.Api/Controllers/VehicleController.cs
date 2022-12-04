using Microsoft.AspNetCore.Mvc;
using Transport.Api.Applications;
using AutoMapper;
using Transport.Api.Dtos;

namespace Transport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleApplication _vehicleApplication;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleApplication vehicleApplication,
                                IMapper mapper)
        {
            _vehicleApplication = vehicleApplication;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetVehicles([FromQuery] int numberOfPassengers)
        {
            if (numberOfPassengers <= 0)
                return BadRequest();

            var vehicles = await _vehicleApplication.GetVehiclesAsync(numberOfPassengers);

            if (vehicles.Count == 0 || vehicles == null)
                return NotFound();

            var result = _mapper.Map<List<VehicleDto>>(vehicles).OrderBy(v => v.TotalPrice);

            return Ok(result);
        }
    }
}
