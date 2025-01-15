using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserData request)
    {
        try
        {
            var user = _loginService.GetUsersDataAsync(request.UserName, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username, password.");
            }

            return Ok(new
            {
                Token = "your-jwt-token",
                User = new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = user.Role
                }
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    
}