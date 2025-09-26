using e_library.Models;

namespace e_library.DTOs
{
    public class GenreResult
    {
        public bool success { get; set; }
        public string? error { get; set; }
        public IEnumerable<Genre> genres { get; set; }
        public int? id { get; set; }
        public bool isInternalError { get; set; }
    }
}
