using e_library.DTOs;
using e_library.Models;
using e_library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_library.Controllers
{
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService service)
        {
            _authorService = service;
        }

        [HttpGet("api/authors")]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _authorService.GetAuthors();

            if (result.success)
            {
                return Ok(new { authors = result.authors });
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpPost("api/authors")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO author)
        {
            if (string.IsNullOrWhiteSpace(author.name) || string.IsNullOrWhiteSpace(author.bio))
            {
                return BadRequest(new { message = "Author name and bio are required." });
            }

            var result = await _authorService.CreateAuthor(author.name, author.bio);

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


        [HttpDelete("api/authors/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var result = await _authorService.DeleteAuthor(id);

            if (result.success)
            {
                return Ok(new { message = "Author deleted successfully." });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return BadRequest(new { message = result.error });
        }


        [HttpPut("api/authors")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            if (author == null || author.id <= 0 || string.IsNullOrWhiteSpace(author.name) || string.IsNullOrWhiteSpace(author.bio))
            {
                return BadRequest(new { success = false, error = "All fields are required." });
            }

            var result = await _authorService.UpdateAuthor(author);

            if (result.success)
            {
                return Ok(new { message = "Author updated successfully." });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return BadRequest(new { message = result.error });
        }

    }

}
