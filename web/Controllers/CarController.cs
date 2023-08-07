using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using web.infrastructure.Data;
using System.Collections.Generic;
using web.core;
using web.core.Entities;
using web.core.Interfaces;
using web.api.Dtos;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using System.Data.Entity;

namespace web.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly IGenericRepository<Car> _carRepo;
        private readonly IMapper _mapper;


        public CarController(IGenericRepository<Car> carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;

        }



        [HttpGet("getall")]
        public async Task<CarListDto> GetListAsync(CarRequestDto request)
        {
            var query = _carRepo.GetQuerable(request.PageIndex, request.PageSize);
            //filtering
            if (!String.IsNullOrEmpty(request.Search))
            {
                query = query.Where(c => c.Number.ToString().Equals(request.Search)
                                           || c.Type.Equals(request.Search)
                                           || c.Color.Equals(request.Search)
                                           || c.WithDriver.ToString().Equals(request.Search)
                                           || c.DailyFare.ToString().Equals(request.Search)
                                           || c.EngineCapacity.ToString().Equals(request.Search));
            }
            //get Count
            var count = query.Count();
            //apply  sorting
            if (!String.IsNullOrEmpty(request.Sort))
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
            }
          
            //apply pagination

            var totalPages = (int)Math.Ceiling((decimal)count / request.PageSize);
            query=query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            
            //output
            var result= await query.ToListAsync();
            CarListDto carList = new CarListDto() { CarsPaginationList=result,Count=count};


            return   carList;
        }
        [HttpGet("getcar/{id}")]
        public async Task<CarDto> GetAsync(Guid id)
        {
            var car = _carRepo.GetById(id);
           
            var result = _mapper.Map<CarDto>(car);

            return (result);
        }


        [HttpPost("insertcar")]
        public async Task<CreateCarDto> CreateAsync(CreateCarDto carDto)

        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                
                _carRepo.Add(car);

            }
            catch (Exception e)
            {
               
            }

            return (carDto);
        }

        [HttpPut("updatecar")]

        public  async Task <UpdateCarDto> UpdateCar(UpdateCarDto carDto)
        {

            try
            {
                var car = _mapper.Map<Car>(carDto);

                _carRepo.Update(car);

            }
            catch (Exception e)
            {

            }

            return (carDto);
        }



        [HttpDelete("deletecar")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                _carRepo.Delete(id);


            }

            catch (Exception e)
            {

                // return BadRequest();
            }
            return Ok();



        }
    }
}
