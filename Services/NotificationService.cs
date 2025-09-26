using e_library.DTOs;
using e_library.Repositories;

namespace e_library.Services
{
    public class NotificationService
    {

        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository repo)
        {
            _notificationRepository = repo;
        }

        public async Task<NotificationResult> GetNotifications(int user_id)
        {
            var result = new NotificationResult();

            try
            {
                var notifications = await _notificationRepository.GetNotifications(user_id);
                result.success = true;
                result.notifications = notifications;
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<NotificationResult> GetNotificationById(int id)
        {
            var result = new NotificationResult();

            try
            {
                var notification = await _notificationRepository.GetNotificationById(id);
                result.success = true;
                result.notification = notification;
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<NotificationResult> CreateNotification(string message, string type, int user_id)
        {
            var result = new NotificationResult();

            try
            {
                var id = await _notificationRepository.AddNewNotification(message, user_id, type, DateTime.UtcNow);

                result.success = true;
                return result;

            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
        }

        public async Task<NotificationResult> DeleteNotification(int id)
        {
            var result = new NotificationResult();

            try
            {
                await _notificationRepository.DeleteNotification(id);
                result.success = true;
                return result;

            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }

        }

        public async Task<NotificationResult> MarkNotificationAsRead(int id)
        {
            var result = new NotificationResult();

            try
            {
                await _notificationRepository.MarkNotificationAsRead(id);
                result.success = true;
                return result;

            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }

        }

    }

}
