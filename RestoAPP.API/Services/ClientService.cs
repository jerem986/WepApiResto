using Microsoft.EntityFrameworkCore;
using RestoAPP.API.DTO;
using RestoAPP.API.DTO.Client;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Mappers;

namespace RestoAPP.API.Services
{
    public class ClientService
    {
        private readonly RestoDbContext dc;

        public ClientService(RestoDbContext dc)
        {
            this.dc = dc;
        }

        public IEnumerable<ClientDetailsDTO> GetClient()
        {
            return dc.Client.MapToList<ClientDetailsDTO>();
        }

        public ClientDetailsDTO GetClientById(int id)
        {
            Client client = dc.Client.FirstOrDefault(c => c.Id == id);
            if (client == null) throw new ArgumentNullException();
            return client.MapTo<ClientDetailsDTO>();
        }

        public bool Edit(ClientDetailsDTO client)
        {
            if (client == null) return false;
            try
            {
                dc.Client.Update(client.MapTo<Client>());
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
