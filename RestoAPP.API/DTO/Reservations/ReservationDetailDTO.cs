using RestoAPP.API.DTO.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Reservations
{
    public class ReservationDetailDTO
    {
        public int Id { get; set; }
        public int NbPers { get; set; }
        public int IdClient { get; set; }
        public int? IdTable { get; set; }
        public DateTime DateDeRes { get; set; }
        public int Horaire { get; set; }
        public ClientDetailsDTO Client { get; set; }

    }
}
