﻿using AutoMapper;
using web.api.Dtos;
using web.core.Entities;

namespace web.api.Profilles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto ,Car>();
            CreateMap<CreateCarDto,CarDto>();
            CreateMap<Car, CreateCarDto>();
            CreateMap<Car, UpdateCarDto>();
        }
    }
}
