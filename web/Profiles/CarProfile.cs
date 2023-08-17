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
            CreateMap<Car, CreateCarDto>();
            CreateMap<Car, UpdateCarDto>();
            CreateMap<UpdateCarDto,CarDto>();
            CreateMap<UpdateCarDto, Car>()
                .ForMember(destination => destination.Type, options => options.Condition(source => !string.IsNullOrEmpty(source.Type)));



          
        }
    }
}
