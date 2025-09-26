using e_library.Models;

namespace e_library.DTOs
{
    public class NotificationResult
    {
        public bool success { get; set; }
        public string? error { get; set; }
        public IEnumerable<Notification> notifications { get; set; }
        public Notification? notification { get; set; }
        public bool isInternalError { get; set; }
    }
}
