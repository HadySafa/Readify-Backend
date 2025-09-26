using e_library.Models;

namespace e_library.DTOs
{
    public class UserResult
    {
        public bool success { get; set; }
        public string? error { get; set; }
        public IEnumerable<User> users { get; set; }

        public User user { get; set; }
        public bool isInternalError { get; set; }
    }
}
