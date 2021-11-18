using RestoAPP.API.DTO.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO
{
    public class RepasAddDTO
    {
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Plat { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public int categoryId { get; set; }

        [Required]
        public int Prix { get; set; }

    }
}
