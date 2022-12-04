using AutoMapper;
using Transport.Api.Dtos;
using Transport.Api.Models;

namespace Transport.Api.Profiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, VehicleDto>()
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.PricePerPassenger * src.VehicleType.MaxPassengers));
        }
    }
}
