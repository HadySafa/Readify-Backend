using Dapper;
using e_library.DTOs;
using e_library.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace e_library.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _connectionString;

        public AuthorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            using var conn = Connection;
            string sql = "SELECT * FROM authors ORDER BY id DESC";
            return await conn.QueryAsync<Author>(sql);
        }

        public async Task<int> AddNewAuthor(string name, string bio)
        {
            using var conn = Connection;
            string sql = @"INSERT INTO authors (name, bio)
                           VALUES (@name, @bio);
                           SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(sql, new { name, bio });
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM authors WHERE name = @name";
            return await conn.QueryFirstOrDefaultAsync<Author>(sql, new { name });
        }

        public async Task DeleteAuthor(int id)
        {
            using var conn = Connection;
            string sql = "DELETE FROM authors WHERE id = @id";
            await conn.ExecuteAsync(sql, new { id });
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            using var conn = Connection;
            var sql = "SELECT * FROM Authors WHERE id = @id";
            return await conn.QueryFirstOrDefaultAsync<Author>(sql, new { id });
        }

        public async Task<int> UpdateAuthor(Author author)
        {
            using var conn = Connection;
            var sql = @"UPDATE Authors 
                    SET name = @name, bio = @bio 
                    WHERE id = @id";
            return await conn.ExecuteAsync(sql, author);
        }

    }
}
