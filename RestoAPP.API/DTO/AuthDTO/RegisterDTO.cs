using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.AuthDTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Tel { get; set; }
    }
}
