using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using web.infrastructure.Data;
using System.Collections.Generic;
using web.core;
using web.core.Entities;
using web.core.Interfaces;
using web.api.Dtos.Incomming;
using web.api.Dtos.Outcomming;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using web.api.Dtos;
using System;
namespace web.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        //private readonly IGenericRepository<Car> _carRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public readonly ICarService _carService;




        public CarController(IMapper mapper, ILogger<CarController> logger, ICarService cartService)
        {
            //_carRepo = carRepo;
            _mapper = mapper;
            _logger = logger;
            _carService = cartService;

        }



        [HttpGet]
        [Consumes("application/json")]
        public async Task<CarListDto> GetListAsync([FromQuery] CarRequestDto request)
        {
            var query = _carService.GetCarsQueryable();
            //filtering
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = FilterCar(query, request);
            }
            //get Count
            var count = await query.CountAsync();
            //apply  sorting
            if (!string.IsNullOrEmpty(request.Sort))
            {

                query = SortCar(query, request);
            }

            //apply pagination

            query = CreatePagination(query, request);

            //output
            var result = await query.ToListAsync();
            var resultDto = _mapper.Map<List<CarDto>>(result);
            CarListDto carListDto = new CarListDto() { CarsPaginationList = resultDto, Count = count };


            return carListDto;
        }

        [HttpGet("{id}")]
        public async Task<CarDto> GetAsync(Guid id)
        {
            var car = await _carService.GetCarByIdAsync(id);

            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }


        [HttpPost]
        public async Task<CarDto> CreateAsync([FromBody] CreateCarDto createCarDto)
        {
            CarDto carDto = null;
            IQueryable<Car> query = null;
            CreateDtoValidator validator = new CreateDtoValidator();            
            ValidationResult resultValidation = validator.Validate(createCarDto);
            if (resultValidation.IsValid)
                {

                    try
                    {
                        query = _carService.GetCarsQueryable();
                        query = query.Where(c => c.Number == createCarDto.Number);


                        if (query.Count()==0)
                        {
                            var carEntity = _mapper.Map<Car>(createCarDto);
                            var result1 = await _carService.AddCarAsync(carEntity);
                            if (result1)
                                carDto = _mapper.Map<CarDto>(carEntity);
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation(e.ToString());
                    }

                
            }
            return carDto;


        }

        [HttpPut("{id}")]

        public async Task<CarDto> UpdateAsync([FromBody] UpdateCarDto updateCarDto)
        {
            //var carExists=await _carService.GetCarByIdAsync(updateCarDto.Id);
            CarDto carDto = null;
            IQueryable<Car> query = null;
            Car carEntity=null;
            bool result1=false;
            CarDto carDtoE=null;
            UpdateDtoValidator validator = new UpdateDtoValidator();            
            ValidationResult resultValidation = validator.Validate(updateCarDto);
            if (resultValidation.IsValid)
                {

                    try
                    {
                        if(updateCarDto.Number>0){
                        query = _carService.GetCarsQueryable();
                        query = query.Where(c => c.Number == updateCarDto.Number);

                        //car isnt found
                        if (query.Count()==0)
                        {
                             carEntity = _mapper.Map<Car>(updateCarDto);
                             result1 = await _carService.UpdateCarAsync(carEntity);
                            if (result1)
                                carDto = _mapper.Map<CarDto>(carEntity);
                            
                        }
                        else{}
                        }//if 
                        else
                        {
                            carEntity = _mapper.Map<Car>(updateCarDto);
                            //carEntity = _mapper.Map<Car>(carDtoE);
                            result1 = await _carService.UpdateCarAsync(carEntity);
                            if (result1)
                                carDto = _mapper.Map<CarDto>(carEntity);
                        
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation(e.ToString());
                    }

                
            }
            return carDto;
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = false;
            try
            {
                result = await _carService.DeleteCarAsync(id);

            }

            catch (Exception e)
            {

                return BadRequest();
            }
            if (result)
                return Ok();
            else
                return NotFound();


        }

        private IQueryable<Car> FilterCar(IQueryable<Car> query, CarRequestDto request)
        {
            query = query.Where(c => c.Number.ToString().Equals(request.Search)
                                           || c.Type.Equals(request.Search)
                                           || c.Color.Equals(request.Search)
                                           || c.WithDriver.ToString().Equals(request.Search)
                                           || c.DailyFare.ToString().Equals(request.Search)
                                           || c.EngineCapacity.ToString().Equals(request.Search));
            return query;
        }

        private IQueryable<Car> CreatePagination(IQueryable<Car> query, CarRequestDto request)
        {
            query = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            return query;
        }

        private IQueryable<Car> SortCar(IQueryable<Car> query, CarRequestDto request)
        {

            switch (request.Sort)
            {
                case "Color_desc":
                    query = query.OrderByDescending(c => c.Color);
                    break;
                case "EngineCapacity_desc":
                    break;
                case "CapacityEngine-asc":
                    break;
                case "Type_desc":
                    query = query.OrderByDescending(c => c.Type);
                    break;
                case "Type_asc":
                    query = query.OrderBy(c => c.Type);
                    break;
                case "WithDriver_desc":
                    query = query.OrderByDescending(c => c.WithDriver);
                    break;
                case "WithDriver_asc":
                    query = query.OrderBy(c => c.WithDriver);
                    break;
                case "DailyFare_desc":
                    query = query.OrderByDescending(c => c.DailyFare);
                    break;
                case "DailyFare_asc":
                    query = query.OrderBy(c => c.DailyFare);
                    break;


                default:
                    query = query.OrderBy(c => c.Color);
                    break;

            }
            return query;
        }

    }
}
