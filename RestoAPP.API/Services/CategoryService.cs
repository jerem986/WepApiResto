using RestoAPP.API.DTO.Category;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Mappers;

namespace RestoAPP.API.Services
{
    public class CategoryService
    {
        private readonly RestoDbContext dc;

        public CategoryService(RestoDbContext dc)
        {
            this.dc = dc;
        }

        public int AddCategory(CategoryAddDTO category)
        {
            if (category == null) throw new ArgumentNullException();
            Category tempCat = new Category
            {
                Type = category.Category
            };
            dc.Add(tempCat);
            dc.SaveChanges();
            return tempCat.Id;
        }

        public CategoryDetailsDTO GetCategoryById(int id)
        {
            if (id < 1) return null;
            Category category = dc.Set<Category>().FirstOrDefault(c => c.Id == id);
            if (category == null) return null;
            return category.MapTo<CategoryDetailsDTO>();
        }

        public IEnumerable<CategoryDetailsDTO> getAllCategory()
        {
            return dc.Category.MapToList<CategoryDetailsDTO>();
        }

        public bool deleteById(int id)
        {
            if (id < 1) return false;
            Category category = dc.Category.First(c => c.Id == id);
            if (category == null) return false;
            dc.Category.Remove(category);
            dc.SaveChanges();
            return true;
        }

        public bool Edit(CategoryDetailsDTO category)
        {
            try
            {
                dc.Category.Update(category.MapTo<Category>());
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
