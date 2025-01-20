
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Database
{
    public class BookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task AddAsync(Book book)
        {
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => b.Isbn == book.Isbn);
            if (existingBook != null)
            {
                Console.WriteLine($"Skipping duplicate book: {book.Title}, ISBN: {book.Isbn}");
                return;
            }

            _context.Book.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddLoanedBookAsync(int userId, LoanedBook loanedBook)
        {
            loanedBook.UserId = userId; 
            _context.LoanedBook.Add(loanedBook); 
            await _context.SaveChangesAsync();
        }
    }
}
