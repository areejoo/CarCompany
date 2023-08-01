using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using web.infrastructure.interfaces;
using web.core;
using web.core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace web.UI.Controllers
{   [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepo;
        private readonly IMemoryCache _memoryCache;


        public CarController(ICarRepository carRepo, IMemoryCache memoryCache)
        {
            _carRepo = carRepo;
            _memoryCache = memoryCache;
       
        }

        //endpoint to get cars from cache
        [HttpGet]
        public  IActionResult GetCarsFromCahce()

        {
            string? value = string.Empty;
             _memoryCache.TryGetValue("my-cars", out value);
            return   Ok(value);
        }

        [HttpGet]
        public async Task<IActionResult> GetCars(string sortOrder, string searchString , string currentFilter, int? pageNumber, int id = 0)
        {
            if (id == 0)
            {
                //sort
                ViewData["ColorSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Color_desc" : "";
                ViewData["CapacityEngineSortParm"] = sortOrder == "CapacityEngineDesc" ? "EngineCapacity_desc" : "CapacityEngine-asc";
                ViewData["TypeSortParm"] = sortOrder == "TypeDesc" ? "Type_desc" : "Type_asc";
                ViewData["WithDriverParm"] = sortOrder == "WithDriverDesc" ? "WithDriver_desc" : "WithDriver_asc";
                ViewData["DailyFareParm"] = sortOrder == "DailyFareDesc" ? "DailyFare_desc" : "DailyFare_asc";
                //pagination 

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                //search 
                ViewData["CurrentFilter"] = searchString;
                var cars = await _carRepo.GetAll();
                //store all cars in cache
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _memoryCache.Set("my-cars", cars, cacheExpiryOptions);
                var SortedCars = from car in cars
                select car;

                if (!String.IsNullOrEmpty(searchString))
                {
                    SortedCars = SortedCars.Where(c => c.Color.Contains(searchString)
                                           || c.Type.Contains(searchString)
                                           || c.WithDriver.Equals(searchString) 
                                           || c.DailyFare==float.Parse(searchString)
                                           || c.EngineCapacity.Equals(searchString)

                                           );
                }
                switch (sortOrder)
                {
                    case "Color_desc":
                        SortedCars = SortedCars.OrderByDescending(c => c.Color);
                        break;
                        case "EngineCapacity_desc":
                        SortedCars = SortedCars.OrderByDescending(c => c.EngineCapacity);
                        break;
                        case "CapacityEngine-asc":
                        SortedCars = SortedCars.OrderBy(c => c.EngineCapacity);
                        break;
                        case "Type_desc":
                        SortedCars = SortedCars.OrderByDescending(c => c.Type);
                        break;
                        case "Type_asc":
                        SortedCars = SortedCars.OrderBy(c => c.Type);
                        break;
                        case "WithDriver_desc":
                        SortedCars = SortedCars.OrderByDescending(c => c.WithDriver);
                        break;
                        case "WithDriver_asc":
                        SortedCars = SortedCars.OrderBy(c => c.WithDriver);
                        break;
                        case "DailyFare_desc":
                        SortedCars = SortedCars.OrderByDescending(c => c.DailyFare);
                        break;
                        case "DailyFare_asc":
                        SortedCars = SortedCars.OrderBy(c => c.DailyFare);
                        break;


                    default:
                        SortedCars = SortedCars.OrderBy(c => c.Color);
                        break;
                }
               
             
                return Ok( cars.ToList());
            }
            else
            {
                var car = await _carRepo.GetById(id);
                return Ok(car);



            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCar(Car model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _carRepo.Add(model);
                    TempData["successMeassage"] = "add succsessfulu....!";
                    return Ok("added");
                }
                else
                {
                    TempData["errorMeassage"] = "data is not valid...!";
                    return BadRequest("not added");


                }
            }
            catch (Exception e)
            {
                TempData["errorMeassage"] = e.Message;
                return BadRequest(e.Message);
            }
            

        }

        //[HttpPut]
        //[Route("{id:guid}")]
        //public  Task<IActionResult> UpdateCar([FromRoute]Guid id,UpdateCarRequest updateCarRequest) {
        //var car =  _carRepo.GetById(id);
        //if (car != null)
        //{ 
        //car.Type=updateCarRequest.WithDriver,

        //}




        [HttpPut]
        public async Task<IActionResult> UpdateCar(Car model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _carRepo.Update(model);
                    TempData["successMeassage"] = "updated succsessfulu....!";
                    return Ok("updated");
                }
                else
                {
                    TempData["errorMeassage"] = "data is not valid...!";
                    return Ok("not updated");


                }
            }
            catch (Exception e)
            {
                TempData["errorMeassage"] = e.Message;
                return BadRequest(e.Message);

            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                Car car = await _carRepo.GetById(id);
                if (car!=null)
                {
                    await _carRepo.Delete(id);
                    TempData["successMeassage"] = "deleted succsessfulu....!";
                    return Ok("deleted");

                }
                else
                {

                    TempData["errorMeassage"] = "car not found ....!";
                    return Ok("not valid");
                }

            }

            catch (Exception e) {

                TempData["errorMeassage"] = e.Message;
                return BadRequest(e.Message);
            }
            }
        

        }
    }
