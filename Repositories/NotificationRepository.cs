using Dapper;
using e_library.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace e_library.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection { get { return new MySqlConnection(_connectionString); } }

        public async Task<IEnumerable<Notification>> GetNotifications(int user_id)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM notifications WHERE user_id = " + user_id + " OR user_id IS NULL ORDER BY id DESC ";
            return await conn.QueryAsync<Notification>(sql);
        }

        public async Task<Notification?> GetNotificationById(int notification_id)
        {
            using var conn = Connection;
            string sql = "SELECT * FROM notifications WHERE id = @Id";
            return await conn.QuerySingleOrDefaultAsync<Notification>(sql, new { Id = notification_id });
        }

        public async Task<int> AddNewNotification(string message, int? user_id, string type, DateTime created_at)
        {
            using var conn = Connection;

            string sql = @"INSERT INTO notifications (user_id, message, is_read, type, created_at)
                               VALUES (@userId,@message,false,@type,@createdAt);
                               SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(sql, new
            {
                userId = user_id,
                message = message,
                type = type,
                createdAt = created_at
            });
        }

        public async Task DeleteNotification(int id)
        {
            using var conn = Connection;
            string sql = "DELETE FROM notifications WHERE id = @id";
            await conn.ExecuteAsync(sql, new { id = id });
        }

        public async Task MarkNotificationAsRead(int id)
        {
            using var conn = Connection;
            string sql = "UPDATE notifications set is_read = true where id = @id";
            await conn.ExecuteAsync(sql, new { id = id });
        }

    }

}
