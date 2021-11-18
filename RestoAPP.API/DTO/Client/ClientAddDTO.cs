using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.DTO.Client
{
    public class ClientAddDTO
    {
        [Required]
        [MaxLength(255)]
        [MinLength(2)]
        public string Name { get; set; }
        public string Tel { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordClient { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserLevel { get; set; }
    }
}
