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
 
}