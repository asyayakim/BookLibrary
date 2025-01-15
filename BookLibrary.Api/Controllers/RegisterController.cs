using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;
[ApiController]
[Route("api/auth")]
public class RegisterController : ControllerBase
{
    private readonly ILoginService _loginService;

    public RegisterController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserData request)
    {
        try
        {
            var existingUser = _loginService.GetUsersDataAsync(request.UserName, request.Password);
            if (existingUser != null)
            {
                return BadRequest($"User with username '{request.UserName}' already exists.");
            }

            var newUser = await _loginService.AddUserDataAsync(request.UserName, request.Password);

            return Ok(new
            {
                Message = "User registered successfully.",
                User = new
                {
                    Id = newUser.Id,
                    UserName = newUser.UserName,
                    Role = newUser.Role
                }
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during registration: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    
}