using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagement.API.Data;
using InventoryManagement.API.Models.Entities;
using InventoryManagement.API.Models.DTOs;
using InventoryManagement.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string Register(RegistrationDto userDto)
        {
            var exists = _context.Users.Any(u => u.Email == userDto.Email);
            if (exists) return "User already exists";

            var user = new User
            {
                Email = userDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                UserName = userDto.UserName,
                MobileNumber = userDto.MobileNumber,
                UserRole = userDto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "User Registered";
        }

        public string Login(LoginDto model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return "Invalid credentials";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole),
                new Claim("UserId", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
