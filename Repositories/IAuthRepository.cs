using e_library.DTOs;
using e_library.Models;

namespace e_library.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<int> AddNewUser(RegistrationRequest user);
    }
}
