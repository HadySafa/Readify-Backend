using e_library.DTOs;
using e_library.Models;

public class BookResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public bool IsInternalError { get; set; }
    public int? Id { get; set; }
    public IEnumerable<BookResponse>? Books { get; set; }

    public IEnumerable<BorrowingsResponseDTO>? BorrowedBooks { get; set; }

    public IEnumerable<UserBookResponse>? UserBooks { get; set; }

    public UserBookResponse? Book { get; set; }

    public IEnumerable<Stats>? Stats { get; set; }
}
