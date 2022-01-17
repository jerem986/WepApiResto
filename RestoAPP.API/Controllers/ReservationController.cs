using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoAPP.API.Attribut;
using RestoAPP.API.DTO.Reservations;
using RestoAPP.API.Services;
using RestoAPP.API.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
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
            try
            {
            int tempId = _reservationService.AddReservation(reservation);
            return Ok(tempId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                _reservationService.DeleteById(id);
                return NoContent();
            }
            catch(KeyNotFoundException)
            {
                return NotFound("Id pas trouvé");
            }
        }

        [HttpPut]
        [ApiAuthorization("ADMIN")] 
        public IActionResult Edit(ReservationDetailDTO reservation)
        {
            try
            {
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
            catch(KeyNotFoundException)
            {
                return NotFound("Id pas trouvé");
            }
            catch (SmtpFailedRecipientException)
            {
                return BadRequest("Email introuvable");
            }
        }

        [HttpGet("ReservationByMonth")]
        public IActionResult GetMonth(int years, [Range(1,12)]int month)
        {
            try
            {

            return Ok(_reservationService.GetByMonth(month, years));
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpGet("ReservationByIdClient {id}")]
        public IActionResult GetReservationById(int id)
        {
            return Ok(_reservationService.GetReservationByIDClient(id));
        }

        [HttpGet("ReservationByDate/{date}")]
        public IActionResult GetReservationByDate(DateTime date)
        {
            return Ok(_reservationService.GetReservationByDate(date));
        }
    }
}
