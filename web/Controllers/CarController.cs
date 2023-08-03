using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using web.infrastructure.Data;
using web.core;
using web.core.Entities;
using web.api.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace web.api.Controllers
{   [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly GenericRepository<Car> _carRepo;


        public CarController(ICarRepository carRepo, IMemoryCache memoryCache)
        {
            _carRepo = carRepo;
       
        }

       

        [HttpGet("getall")]
        public async  Task<ActionResult<IQueryable<CarListDto>>> GetListAsync()
        {
            var cars = _carRepo.GetAll();
            var results= new List<CarListDto>();
            foreach (var c in cars)
                { results.Add( new CarListDto(){
                    Id= c.Id,
                   Number= c.Number,
                    Type=c.Type,
                    EngineCapacity=c.EngineCapacity,
                    DailyFare=c.DailyFare,
                    Color=c.Color,
                    WithDriver=c.WithDriver,
                    DriverId=c.DriverId});
                };

    return  Ok(results) ;
        }
        [HttpGet("getcar/{id}")]
        public async  Task<ActionResult<<CarDto>> GetAsync(Guid id)
        {
            var car = _carRepo.GetById(id);
            if(car==null)
            {
                return  NotFound();
            }
            var result=new CarDto(){
                    
                   Number= car.Number,
                    Type=car.Type,
                    EngineCapacity=car.EngineCapacity,
                    DailyFare=car.DailyFare,
                    Color=car.Color,
                    WithDriver=car.WithDriver,
                    DriverId=car.DriverId};
   
            return  Ok(result) ;
            }

        
        [HttpPost("insertcar")]
        public async Task<IActionResult<CreateCarDto>> CreateAsync(CarRequestDto carDto)
         {
           try{
        var car=new Car();
        car.Type=carDto.Type;
        car. EngineCapacity=carDto.EngineCapacity;
        car.DailyFare=carDto.DailyFare;
        car. Color=carDto.Color;
        car. WithDriver=carDto.WithDriver;
        car.  DriverId=carDto.DriverId;
        _carRepo.Add(car);

        }
        catch (Exception e) {
            return BadRequest();       
            }
        
        return Created(carDto);
        }

        [HttpPut("updatecar")]
        
        public  Task<IActionResult<updateCarDto>> UpdateCar(CarRequestDto carDto) {

        try{
        var car=new Car();
        car.Id=carDto.Id;
        car.Type=carDto.Type;
        car.EngineCapacity=carDto.EngineCapacity;
        car.DailyFare=carDto.DailyFare;
        car.Color=carDto.Color;
        car.WithDriver=carDto.WithDriver;
        car.DriverId=carDto.DriverId;
        }
         catch (Exception e) {
            // return BadRequest();
             
            }

        return Ok(carDto);
        }


        [HttpDelete("deletecar")]
        public async Task<IActionResult> DeleteCar(Guid id)
        {
            try
            {
                _carRepo.Delete(id);


            }

            catch (Exception e) {

              // return BadRequest();
            }
            return Ok();
            
        

        // }
    }
}
