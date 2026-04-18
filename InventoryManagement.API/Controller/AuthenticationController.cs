using InventoryManagement.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        /*
        {
  "email": "admin2@gmail.com",
  "password": "Admin@123",
  "confirmPassword": "Admin@123",
  "username": "admin",
  "mobileNumber": "9984602681",
  "role": "Admin"
}
        */

       
        [HttpPost("register")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Register([FromBody] RegistrationModel model, [FromQuery] string role = "InventoryManager")
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return BadRequest(new { message = "User already exists" });

            // Ensure role exists
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return StatusCode(500, new { message = errors });
            }

            await _userManager.AddToRoleAsync(user, role);

            return Ok(new { message = $"User registered with role {role}" });
        }






        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Invalid credentials" });

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                roles = userRoles
            });
        }

  




        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully" });
        }



     



        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = _configuration["Jwt:Key"];

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key!)
            );

            return new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}