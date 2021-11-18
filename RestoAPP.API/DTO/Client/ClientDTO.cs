using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.Security.Models;
namespace RestoAPP.API.DTO.Client

{
    public class ClientDTO : IPayload
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }

        public string PasswordClient { get; set; }
        public string UserLevel { get; set; }

        public string Identifier { get { return Id.ToString(); } }
        public IEnumerable<string> Roles { get { yield return UserLevel; } } //yield return va parcourir les Userlevel pour faire son retour et donner la liste
    }
}
