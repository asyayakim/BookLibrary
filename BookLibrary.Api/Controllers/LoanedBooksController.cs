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

    [HttpGet("loaned")]
    public async Task<IActionResult> PrintLoanedBooks(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new { Message = "Invalid user ID." });
        }

        try
        {
            var loanedBooks = await _bookService.GetLoanedBooksByUserAsync(userId);
            if (loanedBooks == null || !loanedBooks.Any())
            {
                return NotFound(new { Message = "No loaned books found for this user." });
            }

            var result = loanedBooks.Select(book => new
            {
                CoverImageUrl = book.CoverImageUrl,
                UserId = book.UserId,
                Isbn = book.Isbn,
                Title = book.Title,
                LoanDate = book.LoanDate,
            });
            return Ok(result);
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching loaned books: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpPost("favorite")]
    public async Task<IActionResult> AddFavoriteBook([FromBody] FavoriteBooks favoriteBooks)
    {
        try
        {
            if (favoriteBooks.UserId <= 0)
            {
                return BadRequest(new { Message = "User ID is missing or invalid." });
            }
            if (string.IsNullOrEmpty(favoriteBooks.Isbn) || string.IsNullOrEmpty(favoriteBooks.Title))
            {
                return BadRequest(new { Message = "Missing required fields (ISBN or BookName)." });
            }

            await _bookService.AddFavoriteBookAsync(favoriteBooks.UserId, favoriteBooks);
            return Ok(new { Message = "Book successfully added to favorite books." });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message }); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during book loaning: {ex.Message}");
            return StatusCode(500, new { Message = "Internal server error." });
        }
    }

    [HttpDelete("deleteBook")]
    public async Task<IActionResult> RemoveLoanedBook([FromQuery] int userId, [FromQuery] string isbn)
    {
        try
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "User ID is missing or invalid." });
            }

            var loanedBook = new LoanedBook { UserId = userId, Isbn = isbn };
            await _bookService.RemoveLoanedBookAsync(loanedBook);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("usersData")]
    public async Task<IActionResult> GetUserData()
    {
        try
        {
            var userDataLoanedBooks = await _bookService.GetAllUserData();
            if (userDataLoanedBooks == null || !userDataLoanedBooks.Any())
            {
                return NotFound(new { Message = "No loaned books found for this user." });
            }

            var result = userDataLoanedBooks.Select(book => new
            {
                CoverImageUrl = book.CoverImageUrl,
                UserId = book.UserId,
                Isbn = book.Isbn,
                Title = book.Title,
                LoanDate = book.LoanDate,
            });
            return Ok(result);
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching loaned books: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("mostLoaned")]
    public async Task<IActionResult> GetMostLoanedBooks()
    {
        try
        {
            var mostLoanedBooks = await _bookService.GetMostLoanedBooksAsync();
            if (mostLoanedBooks == null || !mostLoanedBooks.Any())
            {
                return NotFound(new { Message = "No loaned books found." });
            }

            return Ok(mostLoanedBooks);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching most loaned books: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet("showFavorite")]
    public async Task<IActionResult> PrintFavoriteBooks([FromQuery] int userId)

    {
        if (userId <= 0)
        {
            return BadRequest(new { Message = "Invalid user ID." });
        }

        try
        {
            var favoriteBooks = await _bookService.ShowFavoriteBookAsync(userId);
            if (favoriteBooks == null || !favoriteBooks.Any()) 
            {
                return NotFound(new { Message = "No favorite books found for this user." });
            }

            var result = favoriteBooks.Select(book => new
            {
                CoverImageUrl = book.CoverImageUrl,
                UserId = book.UserId,
                Isbn = book.Isbn,
                Title = book.Title,
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching favorite books: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpDelete("removeFavoriteBook")]
    public async Task<IActionResult> RemoveFavoriteBook([FromQuery] int userId, [FromQuery] string isbn)
    {
        try
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "User ID is missing or invalid." });
            }

            var favBook = new FavoriteBooks() { UserId = userId, Isbn = isbn };
            await _bookService.RemoveFavoriteBookAsync(favBook);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return StatusCode(500, "Internal server error.");
        }
    }
}