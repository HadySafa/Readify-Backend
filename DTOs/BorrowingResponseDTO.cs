namespace e_library.DTOs
{
    public class BorrowingsResponseDTO
    {
        public int borrowing_id { get; set; }
        public int book_id { get; set; }
        public int user_id { get; set; }
        public DateTime borrowed_at { get; set; }
        public DateTime? returned_at { get; set; } 
        public DateTime due_date { get; set; }
        public string title { get; set; }
    }

}
