using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<Repas> Repas { get; set; }
    }
}
