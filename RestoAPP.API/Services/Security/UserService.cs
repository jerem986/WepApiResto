using RestoAPP.API.DTO.Client;
using RestoAPP.API.Utils;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.Services.Security
{
    public class UserService
    {
        private readonly HashService _hashService;
        private readonly RestoDbContext dc;
        private readonly UserRepository _repo;

            public UserService(HashService hashService, RestoDbContext dc, UserRepository repo)
        {
            this._hashService = hashService;
            this.dc = dc;
            this._repo = repo;
            }

        public int Register(ClientDTO client)
        {
            
            if (_repo.GetByEmail(client.Email) != null)
            {
                throw new Exception(); // email deja existant
            }
            string salt = Guid.NewGuid().ToString(); //génère un nouvel GUID
            string hashPassword = _hashService.Hash(client.PasswordClient, salt);
            Client tempClient = new Client
            {
                Email = client.Email,
                Salt = salt,
                UserLevel = "USER",
                PasswordClient = hashPassword,
                Name = client.Name,
                Tel = client.Tel,
            };
            dc.Add(tempClient);
            dc.SaveChanges();

            return tempClient.Id;
        }

        public ClientDTO Login(string email, string password)
        {
            Client client = _repo.GetByEmail(email);
            if (client != null && client.PasswordClient == _hashService.Hash(password, client.Salt))
            {
                ClientDTO clientTemp = new ClientDTO
                {
                    Id = client.Id,
                    Email = client.Email,
                    UserLevel = client.UserLevel,
                    Name = client.Name
                };
                return clientTemp;
            }
            return null;
        }
    }
}
