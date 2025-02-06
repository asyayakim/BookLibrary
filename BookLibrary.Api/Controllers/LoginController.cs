using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpGet]
    public async Task<ActionResult<UserData>> GetUserData()
           {
               try
               {
                   var userData = await _loginService.GetUsersDataAsync();
                   return Ok(userData);
               }
               catch (Exception ex)
               {
                   Console.WriteLine($"Error fetching usersdata: {ex.Message}");
                   return StatusCode(500, "Internal server error.");
               }
           }

           [HttpGet("{id}")]
           public async Task<ActionResult<UserData>> GetUserData(int id)
           {
               var userData = await _loginService.GetUserDataByIdAsync(id);
               if (userData == null)
               {
                   return NotFound();
               }
   
               return Ok(userData);
           }
   
        
           [HttpPost]
           public async Task<ActionResult<UserData>> CreateUserData(UserData userData)
           {
               try
               {
                   await _loginService.AddUserDataAsync(userData);
                   return CreatedAtAction(nameof(GetUserData), new { id = userData.UserName }, userData);
               }
               catch (Exception ex)
               {
                   Console.WriteLine($"Error creating UserData: {ex.Message}");
                   return StatusCode(500, "Internal server error.");
               }
           }
   
      
           [HttpDelete("{id}")]
           public async Task<IActionResult> DeleteUserData(int id)
           {
               try
               {
                   await _loginService.DeleteUserDataAsync(id);
                   return NoContent();
               }
               catch (Exception ex)
               {
                   Console.WriteLine($"Error deleting book: {ex.Message}");
                   return StatusCode(500, "Internal server error.");
               }
           } 
}