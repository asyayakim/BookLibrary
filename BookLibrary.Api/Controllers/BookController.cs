using BookLibrary.Database;
using BookLibrary.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly GoogleBooksService _googleBooksService;

        public BookController(BookService bookService, GoogleBooksService googleBooksService)
        {
            _bookService = bookService;
            _googleBooksService = googleBooksService;
        }
        [HttpGet("fetch-google-books")]
        public async Task<IActionResult> FetchGoogleBooks()
        {
            try
            {
                var books = await _googleBooksService.GetFictionBooksAsync();

                foreach (var book in books)
                {
                    Console.WriteLine($"Saving book to database: {book.Title}, ISBN: {book.Isbn}");
                    await _bookService.AddBookAsync(book);
                }

                return Ok(new { Message = $"{books.Count} books fetched and saved to the database!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching books: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // GET: api/Customer/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                await _bookService.AddBookAsync(book);
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating book: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // Delete a book
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting book: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}