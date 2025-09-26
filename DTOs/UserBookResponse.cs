namespace e_library.DTOs
{
    public class UserBookResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string author { get; set; }
        public string genre { get; set; }
        public Boolean isAvailable { get; set;  }
    }
}
