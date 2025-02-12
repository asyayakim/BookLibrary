using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDataController : ControllerBase
{
    private readonly ILoginService _loginService;

    public UserDataController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpGet]
    public async Task<ActionResult<UserData>> GetAllUserData()
    {
        try
        {
            var userData = await _loginService.GetAllUsersDataAsync();
            if (userData == null)
            {
                return NotFound();
            }
   
            return Ok(userData);
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching usersdata: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpPut("changeUserData")]
    public async Task<IActionResult> ChangeUserData([FromBody] UserData request)
    {
        try
        {
            var existingUser = await _loginService.GetUserDataAsync(request.Id);
            if (existingUser == null)
            {
                return BadRequest($"User with ID '{request.Id}' does not exist.");
            }

            await _loginService.ChangeUserDataAsync(request.UserName, request.Password, request.Id);
            return NoContent();
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