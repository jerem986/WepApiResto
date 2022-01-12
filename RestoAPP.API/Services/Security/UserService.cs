using Google.Apis.Auth;
using RestoAPP.API.DTO.Client;
using RestoAPP.API.Utils;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ToolBox.Security.Services;

namespace RestoAPP.API.Services.Security
{
    public class UserService
    {
        private readonly HashService _hashService;
        private readonly RestoDbContext dc;
        private readonly UserRepository _repo;
        private readonly JwtService _jwtService;

        public UserService(HashService hashService, RestoDbContext dc, UserRepository repo, JwtService jwtService)
        {
            _hashService = hashService;
            this.dc = dc;
            _repo = repo;
            _jwtService = jwtService;
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

        public async Task<string> LoginWithGoogle(string token)
        {
            GoogleJsonWebSignature.Payload payload = await _jwtService.VerifyGoogleToken(token);

            if(payload == null)
            {
                throw new Exception("Invalid google payload");
            }
            Client tempClient = _repo.GetByEmail(payload.Email);
            if(tempClient == null)
            {
                string salt = Guid.NewGuid().ToString();
                string hashpassword = _hashService.Hash(Guid.NewGuid().ToString().Substring(0, 8), salt);
                tempClient = new Client
                {
                    Email = payload.Email,
                    Name = payload.GivenName,
                    UserLevel = "USER",
                    PasswordClient = hashpassword,
                    Salt = salt,
                };
                dc.Add(tempClient);
                dc.SaveChanges();
            }

            ClientDTO clientReturn = new ClientDTO
            {
                Email = tempClient.Email,
                Name = tempClient.Name,
                UserLevel = tempClient.UserLevel,
                Id = tempClient.Id
            };
            return _jwtService.CreateToken(clientReturn);
        }

    }
}
