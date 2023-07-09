using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core;


namespace web.infrastructure.interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAll();
        Task <Car> GetById(int id);
        Task  Add(Car model);
        Task Update(Car model);
        Task Delete(int id);

    }
}
