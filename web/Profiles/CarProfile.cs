using AutoMapper;
using web.api.Dtos;
using web.api.Dtos.Incomming;
using web.api.Dtos.Outcomming;
using web.core.Entities;

namespace web.api.Profilles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<CreateCarDto, Car>();
            CreateMap<Car, UpdateCarDto>();
            CreateMap<UpdateCarDto, Car>();
            //CreateMap<CreateCarDto, Car>();
            CreateMap<CreateCarDto, Car>()
                   .ForMember(dest => dest.WithDriver,
                  opt => opt.MapFrom
                  (src => src.DriverId == null ? false : true));
        }
    }
}
