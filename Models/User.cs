using System.Data;
using System.Text.Json.Serialization;

namespace e_library.Models
{
    public class User
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string username { get; set; }
        [JsonIgnore]
        public string password { get; set; }
        public long phone_number { get; set; }
        public string role { get; set; }
    }
}
