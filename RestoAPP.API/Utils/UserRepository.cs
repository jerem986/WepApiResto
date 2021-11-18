using Microsoft.EntityFrameworkCore;
using RestoAPP.API.DTO.Client;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.API.Utils
{
    public class UserRepository   // class pour méthode de vérifiaction si l'email est deja enregistré dans la db
    {
        private readonly RestoDbContext db;

        public UserRepository(RestoDbContext db)
        {
            this.db = db;
        }

        public Client GetByEmail(string email)
        {
            return db.Client.SingleOrDefault(u => u.Email == email);
        }
    }
}