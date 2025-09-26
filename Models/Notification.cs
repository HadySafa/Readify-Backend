namespace e_library.Models
{
    public class Notification
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public int is_read { get; set; }
        public string type { get; set; }
        public DateTime created_at { get; set; }
    }

}
