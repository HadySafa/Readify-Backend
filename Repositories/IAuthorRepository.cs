using e_library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_library.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<int> AddNewAuthor(string name, string bio);
        Task<Author?> GetAuthorByName(string name);
        Task DeleteAuthor(int id);
        Task<Author?> GetAuthorById(int id);
        Task<int> UpdateAuthor(Author author);
    }
}
