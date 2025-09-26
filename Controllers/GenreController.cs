using e_library.DTOs;
using e_library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_library.Controllers
{
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly GenreService _genreService;

        public GenreController(GenreService service)
        {
            _genreService = service;
        }

        [HttpGet("api/genres")]
        public async Task<IActionResult> GetGenres()
        {
            var result = await _genreService.GetGenres();

            if (result.success)
            {
                return Ok(new { genres = result.genres });
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpPost("api/genres")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genre)
        {
            if (string.IsNullOrWhiteSpace(genre.name))
            {
                return BadRequest(new { message = "Genre name is required." });
            }

            var result = await _genreService.CreateGenre(genre.name);

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

        [HttpDelete("api/genres/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var result = await _genreService.DeleteGenre(id);

            if (result.success)
            {
                return Ok(new { message = "Genre deleted successfully." });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return BadRequest(new { message = result.error });

        }

    }

}
