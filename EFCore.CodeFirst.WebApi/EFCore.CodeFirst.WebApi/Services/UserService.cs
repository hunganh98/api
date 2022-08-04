using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Helpers;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
namespace EFCore.CodeFirst.WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        int NumberOfUsers();
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly JwtConfig _jwtConfig;

        public UserService(IOptions<JwtConfig> jwtConfig, IUserRepository userRepository)
        {
            _jwtConfig = jwtConfig.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userRepository.AuthenticateUser(model);

            // return null if user not found
            if (user == null) return null;
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public User GetById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int NumberOfUsers()
        {
            return _userRepository.NumberOfUsers();
        }
    }
}
