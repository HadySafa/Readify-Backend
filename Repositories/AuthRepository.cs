using Dapper;
using e_library.DTOs;
using e_library.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace e_library.Repositories { 

    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection { get { return new MySqlConnection(_connectionString); } }

        public async Task<User?> GetUserByUsername(string username)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM users WHERE username = @Username";
            return await conn.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<int> AddNewUser(RegistrationRequest user)
        {
                using var conn = Connection;
                string sql = @"INSERT INTO users (full_name, username, password, phone_number, role)
                               VALUES (@FullName, @Username, @Password, @PhoneNumber, @Role);
                               SELECT LAST_INSERT_ID();";
                return await conn.ExecuteScalarAsync<int>(sql, new
                {
                    FullName = user.full_name,
                    Username = user.username,
                    Password = user.password,
                    PhoneNumber = user.phone_number,
                    Role = "user"
                });
        }

    }

}
