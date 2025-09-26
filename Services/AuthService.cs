using BCrypt.Net;
using e_library.DTOs;
using e_library.Models;
using e_library.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Google.Protobuf.Reflection.FieldOptions.Types;

namespace e_library.Services
{
    public class AuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration configuration)
        {
            _authRepository = repo;
            _config = configuration;
        }

        private string GenerateToken(User user)
        {
            // store info about the user (username & role)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<AuthResult> AuthenticateUser(LoginRequest credentials)
        {
            var result = new AuthResult();

            try
            {
                var user = await _authRepository.GetUserByUsername(credentials.username);

                // user not found or password is incorrect -> authentication fails
                if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.password, user.password))
                {
                    result.success = false;
                    result.error = "Invalid username or password";
                    return result;
                }
                // user found + password matches -> generate token
                result.token = GenerateToken(user);
                result.success = true;
                return result;
            }
            catch (Exception ex)
            {
                // exception occurred -> error while processing request
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
            
        }

        public async Task<AuthResult> CreateUser(RegistrationRequest user)
        {
            var result = new AuthResult();

            try
            {
                var userExists = await _authRepository.GetUserByUsername(user.username) != null;

                if (userExists)
                {
                    result.success = false;
                    result.error = "Username is not available.";
                    result.isInternalError = false;
                    return result;
                }

                // hash user password
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

                var id = await _authRepository.AddNewUser(user);

                result.success = true;
                result.id = id;
                return result;

            }
            catch(Exception ex) 
            {
                // exception occurred -> error while processing request
                result.success = false;
                result.error = "Error while processing the request.";
                result.isInternalError = true;
                return result;
            }
        }
    }
}
