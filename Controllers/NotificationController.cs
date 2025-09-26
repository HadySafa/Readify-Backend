using e_library.DTOs;
using e_library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_library.Controllers
{
    [ApiController]
    public class NotificationController : ControllerBase
    {

        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService service)
        {
            _notificationService = service;
        }

        [HttpGet("api/notifications")]
        [Authorize]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var result = await _notificationService.GetNotifications(int.Parse(userId));

            if (result.success)
            {
                return Ok(new { notifications = result.notifications });
            }

            return StatusCode(500, new { message = result.error });
        }

        [HttpDelete("api/notifications/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            if (id <= 0) { return BadRequest(new { message = "Submit a valid Id." }); }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId)) { return Forbid(); }

            var notificationResult = await _notificationService.GetNotificationById(id);

            if (!notificationResult.success) { return StatusCode(500, new { message = notificationResult.error }); }

            var notification = notificationResult.notification;
            if (notification == null) { return NotFound(new { message = "Notification not found." }); }

            if (notification.user_id != userId) { return Forbid(); }

            var deleteResult = await _notificationService.DeleteNotification(id);

            if (deleteResult.success) { return Ok(new { message = "Notification deleted successfully." }); }

            if (deleteResult.isInternalError) { return StatusCode(500, new { message = deleteResult.error }); }

            return BadRequest(new { message = deleteResult.error });
        }


        [HttpPost("api/notifications/{id}")]
        [Authorize]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {

            if (id <= 0) { return BadRequest(new { message = "Submit a valid Id." }); }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId)) { return Forbid(); }

            var notificationResult = await _notificationService.GetNotificationById(id);

            if (!notificationResult.success) { return StatusCode(500, new { message = notificationResult.error }); }

            var notification = notificationResult.notification;
            if (notification == null) { return NotFound(new { message = "Notification not found." }); }

            if (notification.user_id != userId) { return Forbid(); }

            var result = await _notificationService.MarkNotificationAsRead(id);

            if (result.success)
            {
                return Ok(new { message = "State changed successfully." });
            }

            if (result.isInternalError)
            {
                return StatusCode(500, new { message = result.error });
            }

            return BadRequest(new { message = result.error });

        }

    }

}
