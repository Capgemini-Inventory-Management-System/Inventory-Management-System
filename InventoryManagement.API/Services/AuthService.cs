using InventoryManagement.API.Data;
using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
       

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            
        }

        // REGISTER
        public async Task<string> Register(LoginModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return "User already exists";

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            return "User Registered Successfully";
        }




        // LOGIN
        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return null;

            var isValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isValid)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserId", user.Id)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
    }
}