using RestoAPP.API.DTO.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO
{
    public class RepasDetailsDTO
    {
        public int Id { get; set; }
        public string Plat { get; set; }
        public string Description { get; set; }
        public string categoryType { get; set; } //nom de la categorie récupérée grace a l'id --> join
        public int Prix { get; set; }
        public int categoryId { get; set; }
        //public CategoryDetailsDTO category { get; set; }
    }
}
