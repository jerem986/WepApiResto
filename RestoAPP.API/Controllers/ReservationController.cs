using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.DTO.Reservations;
using RestoAPP.API.Services;
using RestoAPP.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace RestoAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly MailService _mailService;
        private readonly ClientService _clientService;

        public ReservationController(ReservationService reservationService, MailService mailService, ClientService clientService)
        {
            _reservationService = reservationService;
            _mailService = mailService;
            _clientService = clientService;
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
            if(reservation == null)
            {
                return Problem();
            }
            bool tempBool = false;
            using (TransactionScope scope = new TransactionScope())
            {
                tempBool = _reservationService.Edit(reservation);
                string subject = "Votre réservation du " + reservation.DateDeRes.ToString("dd/MM/yyyy") + " est confirmée";
                string content = "<div>" +
                    "Bonjour" +
                    $"<p>Nous vous confirmons via cet email que réservation du {reservation.DateDeRes.ToString("dd / MM / yyyy")} pour {reservation.NbPers} persone(s)</p>" +
                    "<br>" +
                    "<p>Amicalement</p>" +
                    "<br>" +
                    "<h4>L'équipe du restaurant Gusteau </h4>" +
                    "</div>";
                _mailService.SendEmail(subject, content, _clientService.GetClientById(reservation.IdClient).Email);
                scope.Complete();// ne pas oublier sinon ça va rollback ce qui est dans le scope
            }
            return Ok(tempBool);
        }

        [HttpGet("ReservationByMonth")]
        public IActionResult GetMonth(int years, int month)
        {
            return Ok(_reservationService.GetByMonth(month, years));
        }

        [HttpGet("ReservationById {id}")]
        public IActionResult GetReservationById(int id)
        {
            return Ok(_reservationService.GetReservationByID(id));
        }

        [HttpGet("ReservationByDate/{date}")]
        public IActionResult GetReservationByDate(DateTime date)
        {
            return Ok(_reservationService.GetReservationByDate(date));
        }
    }
}
