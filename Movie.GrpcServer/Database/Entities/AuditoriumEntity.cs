using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.GrpcServer.Database.Entities
{
    public class AuditoriumEntity
    {
        public int Id { get; set; }
        public List<ShowtimeEntity> Showtimes { get; set; }
        public ICollection<SeatEntity> Seats { get; set; }
       
    }
}
