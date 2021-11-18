using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Entities
{
    public class Repas
    {
        public int Id { get; set; }
        public string Plat { get; set; }
        public string Description { get; set; }
        public int Prix  { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
