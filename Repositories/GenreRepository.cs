using Dapper;
using e_library.DTOs;
using e_library.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace e_library.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly string _connectionString;

        public GenreRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection { get { return new MySqlConnection(_connectionString); } }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            using var conn = Connection;
            string sql = "SELECT * FROM genres";
            return await conn.QueryAsync<Genre>(sql);
        }

        public async Task<int> AddNewGenre(string name)
        {
            using var conn = Connection;

            string sql = @"INSERT INTO genres (name)
                               VALUES (@name);
                               SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(sql, new
            {
                name = name
            });
        }

        public async Task<Genre?> GetGenreByName(string name)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM genres WHERE name = @name";
            return await conn.QueryFirstOrDefaultAsync<Genre>(sql, new { name = name });
        }

        public async Task DeleteGenre(int id)
        {
            using var conn = Connection;
            string sql = "DELETE FROM genres WHERE id = @id";
            await conn.ExecuteAsync(sql, new { id = id });
        }

    }

}
