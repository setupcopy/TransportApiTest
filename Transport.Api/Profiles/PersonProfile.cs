using AutoMapper;
using Transport.Api.Dtos;
using Transport.Api.Models;

namespace Transport.Api.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
