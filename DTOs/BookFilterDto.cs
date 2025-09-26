namespace e_library.DTOs
{
    public class BookFilterDto
    {
        public string? Search { get; set; }
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }
        public string? Availability { get; set; } // "All", "Available", "Unavailable"
    }
}
