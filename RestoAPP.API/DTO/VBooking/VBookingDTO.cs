using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.VBooking
{
    public class VBookingDTO
    {
        public DateTime DateDeRes { get; set; }
        public bool IsNoon { get; set; }
        public int Total { get; set; }
    }
}
