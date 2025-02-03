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
            var allLoanedBook = new AllLoanedBooks
            {
                UserId = loanedBook.UserId,
                Isbn = loanedBook.Isbn,
                Title = loanedBook.Title,
                LoanDate = loanedBook.LoanDate,
                CoverImageUrl = loanedBook.CoverImageUrl
            };

            _context.AllLoanedBooks.Add(allLoanedBook);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanedBook>> GetLoanedBooksByUserAsync(int userId)
        {
            return await _context.LoanedBook.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task AddFavoriteBookAsync(int favoriteBooksUserId, FavoriteBooks? favoriteBooks)
        {
            favoriteBooks.UserId = favoriteBooksUserId;
            _context.FavoriteBooks.Add(favoriteBooks);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookIsbnAsync(LoanedBook loanedBook)
        {
            var book = await _context.LoanedBook.FirstOrDefaultAsync(b => b.Isbn == loanedBook.Isbn
                                                                          && b.UserId == loanedBook.UserId);
            if (book != null)
            {
                _context.LoanedBook.Remove(book);
                await _context.SaveChangesAsync();
                Console.WriteLine("Loaned book successfully deleted.");
            }
        }

        public async Task<IEnumerable<LoanedBook>> GetAllLoanedBooksByUserAsync()
        {
            return await _context.LoanedBook.ToListAsync();
        }

        public async Task<IEnumerable<AllLoanedBooks>> GetMostLoanedBooksAsync()
        {
            return await _context.AllLoanedBooks
                .GroupBy(l => new { l.Isbn, l.Title })
                .OrderByDescending(g => g.Count())
                .Select(g => new AllLoanedBooks
                {
                    Isbn = g.Key.Isbn,
                    Title = g.Key.Title,
                    Id = g.First().Id,
                    LoanDate = g.Max(l => l.LoanDate),
                    UserId = g.First().UserId,
                    CoverImageUrl = g.First().CoverImageUrl
                })
                .Take(10) 
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<FavoriteBooks>> GetFavoriteBookAsync(int userId)
        {
            return await _context.FavoriteBooks
                .Where(f => f.UserId == userId) 
                .ToListAsync();
        }
    }
}