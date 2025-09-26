using Dapper;
using e_library.DTOs;
using e_library.Models;
using Microsoft.VisualBasic;
// using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Data;
using System.Net;

namespace e_library.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<BookResponse>> GetBooks()
        {
            using var conn = Connection;
            string sql = "SELECT id, authors.author, genres.genre, title, description, authors.author_id, genres.genre_id, number_of_copies, available_copies FROM `books` INNER JOIN (select id as genre_id, name as genre FROM genres) AS genres INNER JOIN (select id as author_id, name as author FROM authors) AS authors where books.genre_id = genres.genre_id AND books.author_id = authors.author_id ORDER BY id DESC";
            return await conn.QueryAsync<BookResponse>(sql);
        }

        public async Task<IEnumerable<UserBookResponse>> GetBooksForUsers()
        {
            using var conn = Connection;
            string sql = "SELECT books.id, a.author, g.genre, books.title, books.description, CASE WHEN books.available_copies > 0 THEN 1 ELSE 0 END AS isAvailable FROM books INNER JOIN (SELECT id AS genre_id, name AS genre FROM genres) AS g ON books.genre_id = g.genre_id INNER JOIN (SELECT id AS author_id, name AS author FROM authors) AS a ON books.author_id = a.author_id;";
            return await conn.QueryAsync<UserBookResponse>(sql);
        }

        public async Task<IEnumerable<Stats>> GetStats()
        {
            using var conn = Connection;
            string sql = "SELECT COUNT(*) AS totalBooks,SUM(CASE WHEN available_copies > 0 THEN 1 ELSE 0 END) AS availableBooks,SUM(CASE WHEN available_copies = 0 THEN 1 ELSE 0 END) AS borrowedBooks,(SELECT COUNT(*) FROM genres) AS genresCount,(SELECT COUNT(*) FROM authors) AS totalAuthors,(SELECT COUNT(*) FROM users where role = 'user') AS totalUsers FROM books;";
            return await conn.QueryAsync<Stats>(sql);
        }

        public async Task<IEnumerable<BookResponse>> GetBooksBySearch(string searchParameter)
        {
            using var conn = Connection;
            var sql = @"
                SELECT b.id, a.id AS author_id, a.name AS author, g.id AS genre_id, g.name AS genre,
                       b.title, b.description, b.number_of_copies, b.available_copies
                FROM books b
                INNER JOIN genres g ON b.genre_id = g.id
                INNER JOIN authors a ON b.author_id = a.id
                WHERE b.title LIKE @Search";

            return await conn.QueryAsync<BookResponse>(sql, new { Search = "%" + searchParameter + "%" });
        }

        public async Task<IEnumerable<BorrowingsResponseDTO>> GetCurrentlyBorrowedBooks(int user_id)
        {
            using var conn = Connection;
            var sql = @"
                        SELECT 
                            borrowings.user_id, 
                            borrowings.id as borrowing_id, 
                            borrowings.book_id, 
                            borrowings.borrowed_at, 
                            borrowings.returned_at, 
                            borrowings.due_date, 
                            books.title
                        FROM borrowings
                        LEFT JOIN books ON borrowings.book_id = books.id
                        WHERE borrowings.user_id = @UserId AND returned_At IS null;
                    ";
            return await conn.QueryAsync<BorrowingsResponseDTO>(sql, new { UserId = user_id });
        }

        public async Task<IEnumerable<UserBookResponse>> GetFilteredBooks(BookFilterDto filter)
        {
            using var conn = Connection;

            var sql = @"
                        SELECT 
                            books.id, 
                            a.author, 
                            g.genre, 
                            books.title, 
                            books.description, 
                            CASE WHEN books.available_copies > 0 THEN 1 ELSE 0 END AS isAvailable
                        FROM books
                        INNER JOIN (SELECT id AS genre_id, name AS genre FROM genres) AS g 
                            ON books.genre_id = g.genre_id
                        INNER JOIN (SELECT id AS author_id, name AS author FROM authors) AS a
                            ON books.author_id = a.author_id
                        WHERE 1=1
                    ";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                sql += " AND books.title LIKE @Search";
                parameters.Add("Search", $"%{filter.Search}%");
            }

            if (!string.IsNullOrEmpty(filter.GenreId.ToString()))
            {
                sql += " AND g.genre_id = @Genre";
                parameters.Add("Genre", filter.GenreId);
            }

            if (!string.IsNullOrEmpty(filter.AuthorId.ToString()))
            {
                sql += " AND a.author_id = @Author";
                parameters.Add("Author", filter.AuthorId);
            }

            if (!string.IsNullOrEmpty(filter.Availability))
            {
                sql += filter.Availability == "Available"
                    ? " AND books.available_copies > 0"
                    : " AND books.available_copies = 0";
            }

            return await conn.QueryAsync<UserBookResponse>(sql, parameters);

        }

        public async Task<IEnumerable<BorrowingsResponseDTO>> GetCurrentlyBorrowedBooks()
        {
            using var conn = Connection;
            var sql = @"
                        SELECT 
                            borrowings.user_id, 
                            borrowings.id as borrowing_id, 
                            borrowings.book_id, 
                            borrowings.borrowed_at, 
                            borrowings.returned_at, 
                            borrowings.due_date, 
                            books.title
                        FROM borrowings
                        LEFT JOIN books ON borrowings.book_id = books.id
                        WHERE returned_At IS null;
                    ";
            return await conn.QueryAsync<BorrowingsResponseDTO>(sql);
        }

        public async Task<IEnumerable<BorrowingsResponseDTO>> GetBorrowedBooks(int user_id)
        {
            using var conn = Connection;
            var sql = @"
                        SELECT 
                            borrowings.user_id, 
                            borrowings.id as borrowing_id, 
                            borrowings.book_id, 
                            borrowings.borrowed_at, 
                            borrowings.returned_at, 
                            borrowings.due_date, 
                            books.title
                        FROM borrowings
                        LEFT JOIN books ON borrowings.book_id = books.id
                        WHERE borrowings.user_id = @UserId
                        ORDER BY borrowings.borrowed_at DESC;
                    ";
            return await conn.QueryAsync<BorrowingsResponseDTO>(sql, new { UserId = user_id });
        }

        public async Task<int> AddNewBook(BookDTO book)
        {
            using var conn = Connection;
            string sql = @"
                INSERT INTO books (author_id, title, description, genre_id, number_of_copies, available_copies)
                VALUES (@author_id, @title, @description, @genre_id, @number_of_copies, @number_of_copies);
                SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(sql, book);
        }

        // Borrow a book & decrease the quantity of the available copies
        public async Task BorrowBook(int user_id, int book_id, DateTime due_date, DateTime borrowed_at)
        {
            using var conn = (MySqlConnection)Connection;
            {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert into Borrowings
                        var insertSql = @"
                            INSERT INTO borrowings (user_id, book_id, borrowed_at, due_date)
                            VALUES (@UserId, @BookId, @BorrowedAt, @DueDate);";

                        await conn.ExecuteAsync(insertSql, new
                        {
                            UserId = user_id,
                            BookId = book_id,
                            BorrowedAt = borrowed_at,
                            DueDate = due_date
                        }, transaction);

                        // Decrease the available copies of the book
                        var updateSql = @"
                            UPDATE books
                            SET available_copies = available_copies - 1
                            WHERE id = @BookId;";

                        await conn.ExecuteAsync(updateSql, new { BookId = book_id }, transaction);

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // if an error occurs, roll back the transaction and throw an error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


        // Return a book & increase the quantity of the available copies
        public async Task ReturnBook(int borrow_id, int book_id, DateTime returned_at)
        {
            using var conn = (MySqlConnection)Connection;
            {
                await conn.OpenAsync();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Update returned_at in borrowings
                        var insertSql = @"
                            Update borrowings 
                            SET returned_at = @returnedAt
                            where id = @id;";

                        await conn.ExecuteAsync(insertSql, new
                        {
                            returnedAt = returned_at,
                            id = borrow_id
                        }, transaction);

                        // Increase the available copies of the book
                        var updateSql = @"
                            UPDATE books
                            SET available_copies = available_copies + 1
                            WHERE id = @BookId;";

                        await conn.ExecuteAsync(updateSql, new { BookId = book_id }, transaction);

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // If an error occurs, roll back the transaction and throw an error
                        transaction.Rollback();
                        throw;
                    }
                }
            }

        }

        public async Task<Book?> GetBookById(int id)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM books WHERE id = @id";
            return await conn.QueryFirstOrDefaultAsync<Book>(sql, new { id });
        }

        public async Task<BorrowingsResponseDTO?> GetBorrowById(int id)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM borrowings WHERE id = @id";
            return await conn.QueryFirstOrDefaultAsync<BorrowingsResponseDTO>(sql, new { id });
        }

        public async Task<UserBookResponse?> GetBookByIdForUser(int id)
        {
            using var conn = Connection;
            var sql = @"
                        SELECT 
                            books.id, 
                            a.author, 
                            g.genre, 
                            books.title, 
                            books.description, 
                            CASE WHEN books.available_copies > 0 THEN 1 ELSE 0 END AS isAvailable
                        FROM books
                        INNER JOIN (SELECT id AS genre_id, name AS genre FROM genres) AS g 
                            ON books.genre_id = g.genre_id
                        INNER JOIN (SELECT id AS author_id, name AS author FROM authors) AS a
                            ON books.author_id = a.author_id
                        WHERE books.id = @BookId;
                        ";
            return await conn.QuerySingleOrDefaultAsync<UserBookResponse>(sql, new { BookId = id });
        }

        public async Task<int> UpdateBook(Book book)
        {
            using var conn = Connection;
            string sql = @"
                UPDATE books 
                SET author_id = @author_id, title = @title, description = @description, 
                    genre_id = @genre_id, number_of_copies = @number_of_copies, available_copies = @available_copies
                WHERE id = @Id";

            return await conn.ExecuteAsync(sql, book);
        }

        public async Task DeleteBook(int id)
        {
            using var conn = Connection;
            string sql = "DELETE FROM books WHERE id = @id";
            await conn.ExecuteAsync(sql, new { id });
        }

    }

}
