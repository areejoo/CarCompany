using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core;
using web.core.Models;
using web.core.Data;
using web.infrastructure.interfaces;
using Microsoft.EntityFrameworkCore;


namespace web.infrastructure.implements
{

    public class CarRepository : ICarRepository
    {
        private readonly MyAppDbContext _context;

        public CarRepository(MyAppDbContext context)
        {
            _context = context;
        }

       public  async Task Add(Car model)
        {
          await  _context.AddAsync(model);
            await Save();
             
        }

        public async Task Delete(int id)
        {
            Car car = await _context.Cars.FindAsync(id);
            if (car != null) { 
             _context.Cars.Remove(car);
            await Save();
             }
        }

        public  async Task<IEnumerable<Car>> GetAll()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
           
        }

        public async Task<Car> GetById( int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Car model)
            
        {
            Car car = await  _context.Cars.FindAsync(model.Id);
            if (car !=null)
            {
                car.Type = model.Type;
                car.Color = model.Color;
                car.WithDriver = model.WithDriver;
                car.DailyFare = model.DailyFare;
                car.Driver = model.Driver;
                car.EngineCapacity = model.EngineCapacity;
                 _context.Update(model);
                await Save();

            }
           
        }

        private async Task Save() {
            await _context.SaveChangesAsync();
        }
    }
}
