using System.ComponentModel.DataAnnotations;
using web.core.Entities;

namespace web.api.Dtos.Outcomming
{
    public class CarListDto
    {

        public IReadOnlyList<CarDto> CarsPaginationList { get; set; }

        public int Count { get; set; }
    }
}