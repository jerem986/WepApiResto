using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Client
{
    public class ClientDetailsDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Tel { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string UserLevel { get; set; }
        public string Token { get; set; }
    }
}

