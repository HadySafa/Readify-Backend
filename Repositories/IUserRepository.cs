using e_library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_library.Repositories
{
    public interface IUserRepository    
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsersBySearch(string searchParameter);
        Task<User> GetUser(int id);
        Task<int> UpdateUserFullName(string name, int id);
    }
}
