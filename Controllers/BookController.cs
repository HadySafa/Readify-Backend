using e_library.DTOs;
using e_library.Models;
using e_library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace e_library.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public BookController(BookService service, UserService userService)
        {
            _bookService = service;
            _userService = userService;
        }


        [HttpPost("api/books")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateBook([FromBody] BookDTO book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.title) || book.author_id <= 0 || book.genre_id <= 0 || book.number_of_copies <= 0)
            {
                return BadRequest(new { message = "All required fields must be provided." });
            }

            var result = await _bookService.CreateBook(book);

            if (result.Success)
            {
                return Ok(new { id = result.Id });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        [HttpPut("api/books")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            if (book == null || book.id <= 0 || string.IsNullOrWhiteSpace(book.title) || book.author_id <= 0 || book.genre_id <= 0 || book.number_of_copies <= 0)
            {
                return BadRequest(new { success = false, error = "All fields are required." });
            }

            var result = await _bookService.UpdateBook(book);

            if (result.Success)
            {
                return Ok(new { message = "Book updated successfully." });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        [HttpGet("api/users-books")]
        public async Task<IActionResult> GetBooksForUsers()
        {
            var result = await _bookService.GetBooksForUsers();

            if (result.Success)
            {
                return Ok(new { books = result.UserBooks });
            }

            return StatusCode(500, new { message = result.Error });
        }

        [HttpGet("api/books/filter")]
        [Authorize]
        public async Task<IActionResult> GetFilteredBooks([FromQuery] BookFilterDto filter)
        {
            var result = await _bookService.GetFilteredBooks(filter);

            if (result.Success)
            {
                return Ok(new { books = result.UserBooks });
            }

            return StatusCode(500, new { message = result.Error });
        }

        [HttpGet("api/books")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookService.GetBooks();

            if (result.Success)
            {
                return Ok(new { books = result.Books });
            }

            return StatusCode(500, new { message = result.Error });
        }

        [HttpGet("api/books/search")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetBooksBySearch([FromQuery] string q)
        {
            var result = await _bookService.GetBooksBySearch(q);

            if (result.Success)
            {
                return Ok(new { books = result.Books });
            }

            return StatusCode(500, new { message = result.Error });
        }

        [HttpGet("api/stats")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _bookService.GetStats();

            if (result.Success)
            {
                return Ok(new { stats = result.Stats });
            }

            return StatusCode(500, new { message = result.Error });
        }


        [HttpGet("api/books/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var result = await _bookService.GetBookById(id);

            if (result.Success)
            {
                return Ok(new { book = result.Book });
            }

            return StatusCode(500, new { message = result.Error });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("api/books/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var result = await _bookService.DeleteBook(id);

            if (result.Success)
            {
                return Ok(new { message = "Book deleted successfully." });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        [HttpPost("api/books/borrow/{id}")]
        [Authorize]
        public async Task<IActionResult> BorrowBook([FromQuery] int days, int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _bookService.BorrowBook(int.Parse(userId), id, days);

            if (result.Success)
            {
                return Ok(new { message = "Book borrowed successfully." });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        [HttpPost("api/books/return/{id}")]
        [Authorize]
        public async Task<IActionResult> ReturnBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Submit a valid Id." });
            }

            var result = await _bookService.ReturnBook(id);

            if (result.Success)
            {
                return Ok(new { message = "Book returned successfully." });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        // Get currently borrowed books
        [HttpGet("api/books/borrowings/current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentlyBorrowedBooks()
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _bookService.GetCurrentlyBorrowedBooks(int.Parse(userId));

            if (result.Success)
            {
                return Ok(new { books = result.BorrowedBooks });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

        [HttpGet("api/books/borrowings/all")]
        [Authorize]
        public async Task<IActionResult> GetBorrowedBooks()
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _bookService.GetBorrowedBooks(int.Parse(userId));

            if (result.Success)
            {
                return Ok(new { books = result.BorrowedBooks });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }


        [HttpGet("api/books/borrowings/all/users/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetUserBorrowedBooks(int id)
        {

            var user = await _userService.GetUser(id);

            if (user.user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            var result = await _bookService.GetBorrowedBooks(id);

            if (result.Success)
            {
                return Ok(new { books = result.BorrowedBooks });
            }

            if (result.IsInternalError)
            {
                return StatusCode(500, new { message = result.Error });
            }

            return BadRequest(new { message = result.Error });
        }

    }

}
