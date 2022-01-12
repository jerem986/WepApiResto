using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.Attribut;
using RestoAPP.API.DTO;
using RestoAPP.API.DTO.Repas;
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
    public class RepasController : ControllerBase
    {
        private RepasService _repasService;

        public RepasController(RepasService repasService)
        {
            _repasService = repasService;
        }

        [HttpPost]
        public IActionResult CreateRepas(RepasAddDTO repas)
        {
            try
            {
                return Ok(_repasService.AddRepas(repas));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult GetRepas()
        {
            try
            {
                return Ok(_repasService.GetRepas());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[HttpGet("{id}")]
        //public IActionResult GetRepasById(int id)
        //{
        //    if (id < 1) return NotFound();
        //    try
        //    {
        //        RepasDetailsDTO repas = _repasService.GetRepasById(id);
        //        return Ok(repas);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            bool temp = _repasService.DeleteById(id);
            return Ok(temp);
        }

        [HttpPut]
        public IActionResult Edit(RepasEditDTO repas)
        {
            try
            {
                return Ok(_repasService.Edit(repas));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRepasByCategory(int id)
        {
            try
            {
                return Ok(_repasService.GetByCategory(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
