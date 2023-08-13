using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.core.Entities;
using web.core.Interfaces;

namespace web.infrastructure.Data
{
    public class CarRepository :GenericRepository<Car>, ICarRepository
    {
        public CarRepository(MyAppDbContext context) : base(context)
        {
        }
    }
}
