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
using Microsoft.AspNetCore.Authorization;

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


        [AllowAnonymous]  
        [HttpGet]
        public async Task<CarListDto> GetListAsync([FromBody]CarRequestDto request)
        {
            var query =   _carRepo.GetQueryable();
            //filtering
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = FilterCar(query, request);
            }
            //get Count
            var count = query.CountAsync();
            //apply  sorting
             if (!string.IsNullOrEmpty(request.Sort)){

            query = SortCar(query,request);
            }

            //apply pagination

            query = CreatePagination(query, request);
            
            //output
            var result= await query.ToListAsync();
            var resultDto = _mapper.Map<List<CarDto>>(result);
            CarListDto carListDto = new CarListDto() { CarsPaginationList=resultDto,Count=await  count};


            return   carListDto;
        }

        [HttpGet("{id}")]
        public async Task<CarDto> GetAsync(Guid id)
        {
            var car = await _carRepo.GetByIdAsync(id);
           
            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }


        [HttpPost]
        public async Task<CarDto> CreateAsync([FromBody] CreateCarDto createCarDto)

        {
            CarDto carDto=null;
            try
            {
                var carEntity = _mapper.Map<Car>(createCarDto);
                
                await _carRepo.AddAsync(carEntity);

                carDto = _mapper.Map<CarDto>(carEntity);


            }
            catch (Exception e)
            {
               
            }

            return carDto;
        }

        [HttpPut("{id}")]

        public  async  Task <CarDto> UpdateAsync([FromBody] UpdateCarDto updateCarDto)
        {
            CarDto carDto = null;

            try
            {
                var carEntity = _mapper.Map<Car>(updateCarDto);

                await  _carRepo.UpdateAsync(carEntity);
                carDto = _mapper.Map<CarDto>(carEntity);

            }
            catch (Exception e)
            {

            }

            return carDto;
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                
              await  _carRepo.DeleteAsync(id);


            }

            catch (Exception e)
            {

                return BadRequest();
            }
            return   Ok();



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
