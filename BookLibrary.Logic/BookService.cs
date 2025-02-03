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

    public async Task AddFavoriteBookAsync(int userId, FavoriteBooks favoriteBooks)
    {
        bool exists = await _bookRepository.IsBookAlreadyFavoritedAsync(userId, favoriteBooks.Isbn);
        if (exists)
        {
            throw new InvalidOperationException("Book is already in favorites.");
        }

        await _bookRepository.AddFavoriteBookAsync(userId, favoriteBooks);
    }
    

    public async Task RemoveLoanedBookAsync(LoanedBook loanedBook)
    {
        await _bookRepository.RemoveBookIsbnAsync(loanedBook);
    }

    public async Task<IEnumerable<LoanedBook>> GetAllUserData()
    {
        return await _bookRepository.GetAllLoanedBooksByUserAsync();
    }

    public async Task<IEnumerable<AllLoanedBooks>> GetMostLoanedBooksAsync()
    {
        return await _bookRepository.GetMostLoanedBooksAsync();
    }

    public async Task<IEnumerable<FavoriteBooks>> ShowFavoriteBookAsync(int userId)
    {
        return await _bookRepository.GetFavoriteBookAsync(userId);
    }

    public async Task RemoveFavoriteBookAsync(FavoriteBooks favBook)
    {
        await _bookRepository.RemoveFavoriteBooks(favBook);
    }
}