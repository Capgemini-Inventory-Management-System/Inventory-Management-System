using InventoryManagement.API.Models.DTOs;
using InventoryManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api")]

/*
 * {
  "email": "admin@gmail.com",
  "password": "123",
  "confirmPassword": "123",
  "username": "admin",
  "mobileNumber": "9999999999",
  "role": "Admin"
}

{
  "email": "manager@gmail.com",
  "password": "123",
  "confirmPassword": "123",
  "username": "manager",
  "mobileNumber": "8888888888",
  "role": "InventoryManager"
}

*/

public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthenticationController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegistrationDto model)
    {
        var result = _auth.Register(model);

        if (result.Contains("exists"))
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto model)
    {
        var token = _auth.Login(model);

        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }
}