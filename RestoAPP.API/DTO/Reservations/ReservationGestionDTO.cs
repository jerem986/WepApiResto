using RestoAPP.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Reservations
{
    public class ReservationGestionDTO
    {
        public int Id { get; set; }
        public int NbPers { get; set; }
        public string Name { get; set; }
        public int IdClient { get; set; }
        public DateTime DateDeRes { get; set; }
        public int Horaire { get; set; }
        public ValidationStatus ValidationStatuts { get; set; }
    }
}
