using e_library.DTOs;
using e_library.Models;
using e_library.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace e_library.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService service)
        {
            _authService = service;
        }


        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest credentials)
        {

            if (string.IsNullOrWhiteSpace(credentials.username) || string.IsNullOrWhiteSpace(credentials.password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var result = await _authService.AuthenticateUser(credentials); 

            if (result.success)
            {
                return Ok( new { message = "logged in successfully.", token = result.token });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return Unauthorized( new { message = result.error});

        }


        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest user)
        {
            if (string.IsNullOrWhiteSpace(user.username) ||
                string.IsNullOrWhiteSpace(user.password) ||
                string.IsNullOrWhiteSpace(user.phone_number) ||
                string.IsNullOrWhiteSpace(user.full_name))
            {
                return BadRequest(new { message = "All fields are required." });
            }
            if (!user.phone_number.All(char.IsDigit))
            {
                return BadRequest(new { message = "Phone number must contain only digits." });
            }
            if (user.password.Length < 6)
            {
                return BadRequest(new { message = "Password must be at least 6 characters long." });
            }

            var result = await _authService.CreateUser(user);

            if (result.success)
            {
                return Ok(new { id = result.id });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return BadRequest(new { message = result.error });

        }

    }

}
