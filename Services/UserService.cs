using e_library.DTOs;
using e_library.Models;
using e_library.Repositories;
using System.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace e_library.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repo)
        {
            _userRepository = repo;
        }

        public async Task<UserResult> GetUsers()
        {
            var result = new UserResult();

            try
            {
                var users = await _userRepository.GetUsers();
                result.success = true;
                result.users = users;
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<UserResult> GetUser(int id)
        {
            var result = new UserResult();

            try
            {
                var user = await _userRepository.GetUser(id);
                result.success = true;
                result.user = user;
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

        public async Task<UserResult> UpdateUserFullName(int id, string name)
        {
            var result = new UserResult();

            try
            {
                var rows = await _userRepository.UpdateUserFullName(name, id);

                if (rows > 0)
                {
                    result.success = true;
                    return result;
                }

                result.success = false;
                result.error = "Update failed.";
                return result;
            }
            catch (Exception)
            {
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
        }

        public async Task<UserResult> GetUsersBySearch(string searchParameter)
        {
            var result = new UserResult();

            try
            {
                var users = await _userRepository.GetUsersBySearch(searchParameter);
                result.success = true;
                result.users = users;
                return result;
            }

            catch (Exception)
            {
                result.success = false;
                result.isInternalError = true;
                result.error = "Error while processing the request.";
                return result;
            }

        }

    }
}
