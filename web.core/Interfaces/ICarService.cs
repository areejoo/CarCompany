using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core.Entities;

namespace web.core.Interfaces
{
    public interface ICarService
    {
        IQueryable<Car> GetCarsQueryable();
        Task<Car> GetCarByIdAsync(Guid id);
        Task <bool> AddCarAsync(Car entity);
        Task <bool> UpdateCarAsync(Car entity);
        Task <bool> DeleteCarAsync(Guid id);
    }
}
