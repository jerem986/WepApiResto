using Microsoft.AspNetCore.Mvc;
using RestoAPP.API.DTO.AuthDTO;
using RestoAPP.API.DTO.Client;
using RestoAPP.API.Services.Security;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Mappers;
using ToolBox.Security.Services;

namespace RestoAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        [Produces(typeof(LoginDTO))] //fonction? a voir 
        public IActionResult Login(LoginDTO login)
        {
            try
            {
                ClientDTO client = _userService.Login(login.Email, login.Password);
                if (client == null) return Unauthorized();
                else return Ok(new LoginTokenDTO
                {
                    Token = _jwtService.CreateToken(client),
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterDTO register)  
        {
            try
            {
                int temp = _userService.Register(new ClientDTO
                {
                    Email = register.Email,
                    PasswordClient = register.Password,
                    Tel = register.Tel,
                    Name = register.Name,
                   
                });
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
