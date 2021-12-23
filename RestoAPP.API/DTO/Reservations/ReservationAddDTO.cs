using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Reservations
{
    public class ReservationAddDTO
    {
        [Required]
        public int NbPers { get; set; }
        [Required]
        public int IdClient { get; set; }
        [Required]
        public DateTime DateDeRes { get; set; }
        [Required]
        [MaxLength(50)]
        public int Horaire { get; set; }
    }
}
