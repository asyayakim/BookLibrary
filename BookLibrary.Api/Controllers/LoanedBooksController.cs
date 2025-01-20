using System.Security.Claims;
using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BookLibrary.Api.Controllers;
[ApiController]
[Route("api/books")]

public class LoanedBooksController : ControllerBase
{
    private readonly BookService _bookService;

    public LoanedBooksController(BookService bookService)
    {
        _bookService = bookService;
    }
    [HttpPost("loan")]
    public async Task<IActionResult> LoanBook([FromBody] LoanedBook loanedBook)
    {
        try
        {
            if (loanedBook.UserId <= 0)
            {
                return BadRequest(new { Message = "User ID is missing or invalid." });
            }

            if (string.IsNullOrEmpty(loanedBook.Isbn) || string.IsNullOrEmpty(loanedBook.Title))
            {
                return BadRequest(new { Message = "Missing required fields (ISBN or BookName)." });
            }

            await _bookService.AddLoanedBookAsync(loanedBook.UserId, loanedBook);
            return Ok(new { Message = "Book successfully loaned." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during book loaning: {ex.Message}");
            return StatusCode(500, new { Message = "Internal server error." });
        }
    }
}