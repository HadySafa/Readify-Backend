using e_library.Models;

namespace e_library.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications(int user_id);
        Task MarkNotificationAsRead(int id);
        Task DeleteNotification(int id);
        Task<int> AddNewNotification(string message, int? user_id, string type, DateTime created_at);
        Task<Notification?> GetNotificationById(int notification_id);
    }
}
