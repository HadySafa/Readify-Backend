namespace e_library.DTOs
{
    public class BookDTO
    {
        public string title { get; set; } = null!;
        public string? description { get; set; }
        public int author_id { get; set; }
        public int genre_id { get; set; }
        public int number_of_copies { get; set; }
    }
}
