using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.DTO.Reservations;
using RestoAPP.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestoAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public IActionResult AddReservation(ReservationAddDTO reservation)
        {
            int tempId = _reservationService.AddReservation(reservation);
            return Ok(tempId);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            _reservationService.DeleteById(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult Edit(ReservationDetailDTO reservation)
        {
            return Ok(_reservationService.Edit(reservation));
        }

        [HttpGet("ReservationByMonth")]
        public IActionResult GetMonth(int years, int month)
        {
            return Ok(_reservationService.GetByMonth(month, years));
        }
    }
}
