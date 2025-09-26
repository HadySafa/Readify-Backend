namespace e_library.DTOs
{
    public class AuthResult
    {
        public bool success { get; set; }       
        public string? token { get; set; }    
        public string? error { get; set; }
        public int? id { get; set; }
        public bool isInternalError { get; set; }
    }
}
