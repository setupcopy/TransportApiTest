using Microsoft.AspNetCore.Mvc;
using Transport.Api.Models;
using AutoMapper;
using Transport.Api.Dtos;

namespace Transport.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMapper _mapper;

        public ContactController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactInfo()
        {
            var person = new Person();
            person.Id = Guid.NewGuid();
            person.FirstName = "Zack";
            person.LastName = "Liu";
            person.Phone = "0403058382";

            var result = _mapper.Map<PersonDto>(person);

            return Ok(result);
        }
    }
}
