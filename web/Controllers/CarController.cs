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



        [HttpGet]
        public async Task<CarListDto> GetListAsync(CarRequestDto request)
        {
            var query =   _carRepo.GetQueryable();
            //filtering
            if (!String.IsNullOrEmpty(request.Search))
            {
                query = FilterCar(query, request);
            }
            //get Count
            var count = query.Count();
            //apply  sorting

            query = SortCar(query,request);

            //apply pagination

            query = CreatePagination(query, request);
            
            //output
            var result= await query.ToListAsync();
            var resultDto = _mapper.Map<CarDto[]>(result);
            CarListDto carList = new CarListDto() { CarsPaginationList=resultDto,Count=count};


            return   carList;
        }

        [HttpGet]
        public async Task<CarDto> GetAsync(Guid id)
        {
            var car = await _carRepo.GetByIdAsync(id);
           
            var result = _mapper.Map<CarDto>(car);

            return (result);
        }


        [HttpPost]
        public async Task<CreateCarDto> CreateAsync(CreateCarDto carDto)

        {
            try
            {
                var car = _mapper.Map<Car>(carDto);
                
                await _carRepo.Add(car);

            }
            catch (Exception e)
            {
               
            }

            return (carDto);
        }

        [HttpPut]

        public  async  Task <UpdateCarDto> UpdateCar(UpdateCarDto carDto)
        {

            try
            {
                var car = _mapper.Map<Car>(carDto);

                await  _carRepo.Update(car);

            }
            catch (Exception e)
            {

            }

            return carDto;
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                
              await  _carRepo.Delete(id);


            }

            catch (Exception e)
            {

                // return BadRequest();
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
            return query;
        }

    }
}
