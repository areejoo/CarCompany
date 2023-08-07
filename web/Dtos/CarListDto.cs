using System.ComponentModel.DataAnnotations;
using web.core.Entities;

namespace web.api.Dtos
{
    public class CarListDto
    {

        public ICollection<CarDto> CarsPaginationList { get; set; }

        public int Count { get; set; }
    }
}