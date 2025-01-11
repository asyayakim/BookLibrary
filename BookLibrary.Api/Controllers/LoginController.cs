using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
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
   
           // GET: api/UserData
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
   
           // POST: api/UserData
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
   
           // Delete a book
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