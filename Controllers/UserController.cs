using e_library.DTOs;
using e_library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.Net;

namespace e_library.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }

        [HttpGet("api/users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();

            if (result.success)
            {
                return Ok(new { users = result.users });
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpGet("api/user")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.GetUser(int.Parse(userId));

            if (result.success)
            {
                return Ok(new { user = result.user });
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpPut("api/user")]
        [Authorize]
        public async Task<IActionResult> UpdateUserFullName([FromBody]  UpdateUserRequest request)
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _userService.UpdateUserFullName(int.Parse(userId), request.full_name);

            if (result.success)
            {
                return Ok();
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpGet("api/users/search")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUsersBySearch([FromQuery] string q)
        {
            var result = await _userService.GetUsersBySearch(q);

            if (result.success)
            {
                return Ok(new { users = result.users });
            }

            return StatusCode(500, new { message = result.error });
        }

    }

}
