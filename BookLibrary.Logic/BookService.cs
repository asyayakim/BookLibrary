using BookLibrary.Database;

namespace BookLibrary.Logic;

public class BookService 
{
    private readonly BookRepository _bookRepository;

    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task AddBookAsync(Book book)
    {
        await _bookRepository.AddAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }

    public async Task AddLoanedBookAsync(int parse, LoanedBook loanedBook)
    {
        await _bookRepository.AddLoanedBookAsync(parse, loanedBook);
    }

    public async Task<IEnumerable<LoanedBook>>  GetLoanedBooksByUserAsync(int userId)
    {
       return await _bookRepository.GetLoanedBooksByUserAsync(userId);
    }

    public async Task AddFavoriteBookAsync(int favoriteBooksUserId, FavoriteBooks favoriteBooks)
    {
       await _bookRepository.AddFavoriteBookAsync(favoriteBooksUserId, favoriteBooks);
    }

    public async Task RemoveLoanedBookAsync(LoanedBook loanedBook)
    {
        await _bookRepository.RemoveBookIsbnAsync(loanedBook);
    }
}