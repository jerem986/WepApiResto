using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.Attribut;
using RestoAPP.API.DTO.Client;
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
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClient()
        {
            return Ok(_clientService.GetClient());
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            if (id < 1) return Problem("Id pas valide");
            return Ok(_clientService.GetClientById(id));
        }

        [HttpPut]
        public IActionResult UpdateClient(ClientDetailsDTO client)
        {
            if (client == null) return NotFound();
            return Ok(_clientService.Edit(client));
        }
    }
}
