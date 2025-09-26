using e_library.Models;
using System.Collections.Generic;

namespace e_library.DTOs
{
    public class AuthorResult
    {
        public bool success { get; set; }           
        public bool isInternalError { get; set; }  
        public string? error { get; set; }        
        public int? id { get; set; }   
        public IEnumerable<Author>? authors { get; set; }  
    }
}
