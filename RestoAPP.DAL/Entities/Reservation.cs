using RestoAPP.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NbPers { get; set; }
        public int IdClient { get; set; }
        public int HeureReservation { get; set; }
        public DateTime DateDeRes { get; set; }
        public string Horaire { get; set; }
        public string ServiceReservation { get; set; }

        public Client Client { get; set; }

        public ValidationStatus ValidationStatuts { get; set; }

    }
}
