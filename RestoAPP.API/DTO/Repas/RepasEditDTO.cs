using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Repas
{
    public class RepasEditDTO
    {
        public int Id { get; set; }
        public string Plat { get; set; }
        public string Description { get; set; }
        public int Prix { get; set; }
        public int CategoryId { get; set; }

    }
}
