using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string PasswordClient { get; set; }
        public string UserLevel { get; set; }
        public string Salt { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}
