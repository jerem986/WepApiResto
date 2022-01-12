using Microsoft.EntityFrameworkCore;
using RestoAPP.API.DTO;
using RestoAPP.API.DTO.Repas;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Mappers;

namespace RestoAPP.API.Services
{
    public class RepasService
    {
        private readonly RestoDbContext dc;

        public RepasService(RestoDbContext dc)
        {
            this.dc = dc;
        }

        public int AddRepas(RepasAddDTO repas)
        {
            if (repas == null) throw new ArgumentNullException();
            Repas tempRepas = new Repas
            {
                CategoryId = repas.categoryId,
                Plat = repas.Plat,
                Prix = repas.Prix,
                Description = repas.Description
            };
            if(tempRepas.CategoryId == 9)
            {
                IEnumerable<RepasDetailsDTO> listRepas = GetByCategory(9);
                if( listRepas.Count() >= 7)
                {
                    throw new Exception("Les 7 repas sont déjà établi");
                }
            }
            dc.Repas.Add(tempRepas);
            dc.SaveChanges();
            return tempRepas.Id;
        }

        public IEnumerable<RepasDetailsDTO> GetRepas()
        {
            IEnumerable<RepasDetailsDTO> repas = dc.Repas.Include(c => c.Category).Select(m => new RepasDetailsDTO
            {
                CategoryId = m.CategoryId,
                categoryType = m.Category.Type,
                Description = m.Description,
                Plat = m.Plat,
                Prix = m.Prix,
                Id = m.Id
            });

            return repas;
        }

        public RepasDetailsDTO GetRepasById(int id)
        {
            Repas repas = dc.Repas.Include(c => c.Category).FirstOrDefault(r => r.Id == id);
            if (repas == null) return null;
            RepasDetailsDTO check = repas.MapTo<RepasDetailsDTO>( m=> {
                m.categoryType = repas.Category.Type; // exemple pour pouvoir mapper plusieurs ligne de code à la suite
                //m.categoryId = repas.CategoryId           ligne useless mais sert d'exemple de plusieurs ligne a mapper
            }); //autre méthode pour mapper avec le map auto mais same GetRepas 
            return check;
        }

        public bool DeleteById(int id)
        {
            if (id < 1) return false;
            Repas repas = dc.Repas.FirstOrDefault(r => r.Id == id);
            //Repas repas = dc.Repas.Find(id); --> équivalent mais find ne fera la recherche que par l'id !!
            if (repas == null) return false;
            dc.Repas.Remove(repas);
            dc.SaveChanges();
            return true;
        }

        public bool Edit(RepasEditDTO repas)
        {
            try
            {
                //Repas tempRepas = dc.Repas.Find(repas.Id);       // c'est l'objet de la db on peut travailler en direct dessus
                //tempRepas.Description = repas.Description;
                //tempRepas.Plat = repas.Plat;
                //tempRepas.Prix = repas.Prix;
                //tempRepas.CategoryId = repas.categoryId;
                //dc.SaveChanges();
                //return true;

                //cela fonctionne sans soucis, on viendrai directement écraser les données dans la db et on sauvegarde

                Repas tempRepas = new Repas
                {
                    Id = repas.Id,
                    CategoryId = repas.CategoryId,
                    Plat = repas.Plat,
                    Prix = repas.Prix,
                    Description = repas.Description
                };
                dc.Update(tempRepas);
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<RepasDetailsDTO> GetByCategory(int id)
        {
            IEnumerable<RepasDetailsDTO> repasList = dc.Repas.Include(c => c.Category).Where(c => c.CategoryId == id).MapToList<RepasDetailsDTO>();
            return repasList;
        }
    }
}
