namespace e_library.DTOs
{
    public class BookResponse
    {
        public int id { get; set; }
        public string title { get; set; } 
        public string description { get; set; } 

        public int author_id { get; set; }
        public string author { get; set; }

        public int genre_id { get; set; }
        public string genre { get; set; }

        public int number_of_copies { get; set; }
        public int available_copies { get; set; }
    }
}
