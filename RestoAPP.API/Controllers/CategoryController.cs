using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.Attribut;
using RestoAPP.API.DTO.Category;
using RestoAPP.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorization("ADMIN")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddDTO category)
        {
            try
            {
                int newId = _categoryService.AddCategory(category);
                return Ok(newId);
            }
            catch(Exception ex )
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (id < 1) return NotFound();
            try
            {
                return Ok(_categoryService.GetCategoryById(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            try
            {
                return Ok(_categoryService.getAllCategory());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_categoryService.deleteById(id));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(CategoryDetailsDTO category)
        {
            try
            {
                return Ok(_categoryService.Edit(category));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}


