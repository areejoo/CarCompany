using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core.Entities;
using web.core.Interfaces;

namespace web.infrastructure.Services
{
    public class CarService : ICarService
    {
        public IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddCarAsync(Car entity)
        {
            await _unitOfWork.Cars.AddAsync(entity);
            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }


        public async Task<bool> DeleteCarAsync(Guid id)
        {
           await _unitOfWork.Cars.DeleteAsync(id);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }


        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            var Car = await _unitOfWork.Cars.GetByIdAsync(id);
            if (Car != null)
            {
                return Car;
            }
            return null;
        }

       public  IQueryable<Car> GetCarsQueryable()
        {
            var carDetailsList = _unitOfWork.Cars.GetQueryable();
            return carDetailsList;

        }

       public async Task <bool>UpdateCarAsync(Car entity)
        {
          await  _unitOfWork.Cars.UpdateAsync(entity);

            var result = _unitOfWork.Save();

            if (result > 0)
                return true;
            else
                return false;
        }
    
    }
}
