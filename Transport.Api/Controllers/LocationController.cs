using Microsoft.AspNetCore.Mvc;
using System.Net;
using Transport.Api.Api;

namespace Transport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocation()
        {
            try
            { 
                var city = await _locationService.GetLocation();

                if (string.IsNullOrWhiteSpace(city))
                    return NotFound();

                return Ok(city);
            }
            catch(Exception ex) //TODO: Exceptions should be handled by Exception filter. Add Aop to address exceptions then remove try/catch from there
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
