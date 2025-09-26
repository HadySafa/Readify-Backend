using Dapper;
using e_library.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace e_library.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection { get { return new MySqlConnection(_connectionString); } }

        public async Task<IEnumerable<User>> GetUsers()
        {
            using var conn = Connection;
            string sql = "SELECT id, full_name, username, phone_number, role FROM users where role='user' ORDER BY id DESC";
            return await conn.QueryAsync<User>(sql);
        }

        public async Task<User> GetUser(int id)
        {
            using var conn = Connection;
            string sql = "SELECT id, full_name, username, phone_number, role FROM users where id = " + id;
            return await conn.QueryFirstOrDefaultAsync<User>(sql);
        }

        public async Task<int> UpdateUserFullName(string name, int id)
        {
            using var conn = Connection;
            var sql = @"UPDATE users 
                    SET full_name = @name
                    WHERE id = @id";
            return await conn.ExecuteAsync(sql, new { name = name, id = id });
        }

        public async Task<IEnumerable<User>> GetUsersBySearch(string searchParameter)
        {
            using var conn = Connection;
            var sql = @"
                    SELECT id, full_name, username, phone_number, role
                    FROM users
                    WHERE full_name LIKE @Search AND role = 'user'
                    ORDER BY id DESC";

            return await conn.QueryAsync<User>(sql, new { Search = "%" + searchParameter + "%" });
        }

    }
}
