using e_library.DTOs;
using e_library.Models;
using e_library.Repositories;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System;
using System.Threading.Tasks;

namespace e_library.Services
{
    public class BookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly INotificationRepository _notificationRepository;

        public BookService(IBookRepository repo, INotificationRepository notificationRepository)
        {
            _bookRepository = repo;
            _notificationRepository = notificationRepository;
        }

        public async Task<BookResult> GetCurrentlyBorrowedBooks(int user_id)
        {
            var result = new BookResult();

            try
            {
                var BorrowedBooks = await _bookRepository.GetCurrentlyBorrowedBooks(user_id);
                result.Success = true;
                result.BorrowedBooks = BorrowedBooks;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<BookResult> GetBorrowedBooks(int user_id)
        {
            var result = new BookResult();

            try
            {
                var BorrowedBooks = await _bookRepository.GetBorrowedBooks(user_id);
                result.Success = true;
                result.BorrowedBooks = BorrowedBooks;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<BookResult> BorrowBook(int user_id, int book_id, int days)
        {
            var MaxBorrowRate = 3;
            var result = new BookResult();


            try
            {
                var BorrowedBooks = await _bookRepository.GetCurrentlyBorrowedBooks(user_id);

                if (BorrowedBooks.Count() >= MaxBorrowRate)
                {
                    // Reached max borrow rate
                    result.Success = false;
                    result.Error = "You have reached the maximum number of borrowings. You cannot borrow more books right now.";
                    return result;
                }

                var Book = await _bookRepository.GetBookById(book_id);

                if (Book == null || Book.available_copies == 0)
                {
                    // Book not available
                    result.Success = false;
                    result.Error = "This book is not available right now. Please try again later.";
                    return result;
                }

                bool exists = BorrowedBooks.Any(Book => Book.book_id == book_id);

                if (exists)
                {
                    // User is already borrowing this book
                    result.Success = false;
                    result.Error = "You are already borrowing this book right now. Please return it before borrowing again.";
                    return result;
                }

                await _bookRepository.BorrowBook(user_id, book_id, DateTime.UtcNow.AddDays(days), DateTime.UtcNow);

                result.Success = true;
                return result;

            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }

        }
        public async Task<BookResult> ReturnBook(int borrow_id)
        {

            var result = new BookResult();

            try
            {

                var Borrow = await _bookRepository.GetBorrowById(borrow_id);

                if (Borrow == null)
                {
                    // Borrow not available
                    result.Success = false;
                    result.Error = "Id is invalid.";
                    return result;
                }

                await _bookRepository.ReturnBook(borrow_id, Borrow.book_id, DateTime.UtcNow);

                result.Success = true;
                return result;

            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<BookResult> GetBookById(int id)
        {
            var result = new BookResult();

            try
            {
                var book = await _bookRepository.GetBookByIdForUser(id);
                result.Success = true;
                result.Book = book;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }


        // Get all books
        public async Task<BookResult> GetBooks()
        {
            var result = new BookResult();

            try
            {
                var books = await _bookRepository.GetBooks();
                result.Success = true;
                result.Books = books;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        // Get filtered books
        public async Task<BookResult> GetFilteredBooks(BookFilterDto obj)
        {
            var result = new BookResult();

            try
            {
                var books = await _bookRepository.GetFilteredBooks(obj);
                result.Success = true;
                result.UserBooks = books;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        public async Task<BookResult> GetBooksForUsers()
        {
            var result = new BookResult();

            try
            {
                var books = await _bookRepository.GetBooksForUsers();
                result.Success = true;
                result.UserBooks = books;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        public async Task<BookResult> GetBooksBySearch(string searchParameter)
        {
            var result = new BookResult();

            try
            {
                var books = await _bookRepository.GetBooksBySearch(searchParameter);
                result.Success = true;
                result.Books = books;
                return result;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<BookResult> GetStats()
        {
            var result = new BookResult();

            try
            {
                var stats = await _bookRepository.GetStats();
                result.Success = true;
                result.Stats = stats;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        public async Task<BookResult> UpdateBook(Book updatedBook)
        {
            var result = new BookResult();
            try
            {
                var existing = await _bookRepository.GetBookById(updatedBook.id);
                if (existing == null)
                {
                    result.Success = false;
                    result.Error = "Book not found.";
                    return result;
                }

                var borrowedBooks = existing.number_of_copies - existing.available_copies;

                if (updatedBook.number_of_copies < borrowedBooks)
                {
                    result.Success = false;
                    result.Error = "The number of copies cannot be set lower than the number of borrowed books.";
                    return result;
                }

                updatedBook.available_copies = updatedBook.number_of_copies - borrowedBooks;

                await _bookRepository.UpdateBook(updatedBook);
                result.Success = true;
                return result;

            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        public async Task<BookResult> CreateBook(BookDTO newBook)
        {
            var result = new BookResult();

            try
            {
                var id = await _bookRepository.AddNewBook(newBook);
                result.Success = true;
                // add notification
                try
                {
                    await _notificationRepository.AddNewNotification($"New book {newBook.title} has been added.", null, "announcement", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    // do nothing
                }
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

        // Delete a book
        public async Task<BookResult> DeleteBook(int id)
        {
            var result = new BookResult();
            try
            {
                await _bookRepository.DeleteBook(id);
                result.Success = true;
                return result;
            }
            catch (Exception)
            {
                result.Success = false;
                result.IsInternalError = true;
                result.Error = "Error while processing the request.";
                return result;
            }
        }

    }
}
