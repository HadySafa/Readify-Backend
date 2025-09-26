using e_library.DTOs;
using e_library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_library.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookResponse>> GetBooks();
        Task<Book?> GetBookById(int id);
        Task<int> AddNewBook(BookDTO book);
        Task<int> UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<IEnumerable<UserBookResponse>> GetFilteredBooks(BookFilterDto filter);
        Task<IEnumerable<UserBookResponse>> GetBooksForUsers();
        Task<IEnumerable<BorrowingsResponseDTO>> GetBorrowedBooks(int user_id);
        Task<IEnumerable<Stats>> GetStats();
        Task<IEnumerable<BookResponse>> GetBooksBySearch(string searchParameter);
        Task<IEnumerable<BorrowingsResponseDTO>> GetCurrentlyBorrowedBooks(int user_id);
        Task<IEnumerable<BorrowingsResponseDTO>> GetCurrentlyBorrowedBooks();
        Task<UserBookResponse?> GetBookByIdForUser(int id);
        Task BorrowBook(int user_id, int book_id, DateTime due_date, DateTime borrowed_at);
        Task ReturnBook(int borrow_id, int book_id, DateTime returned_at);
        Task<BorrowingsResponseDTO?> GetBorrowById(int id);
    }
}
