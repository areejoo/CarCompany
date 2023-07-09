using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web.core
{
    public  class UpdateCarRequest
    {
        public float EngineCapacity { get; set; }
        public String Color { get; set; }
        public String Type { get; set; }
        public String DailyFare { get; set; }
        public bool WithDriver { get; set; }
        public ICollection<Driver>? Drivers { get; set; }
    }
}
