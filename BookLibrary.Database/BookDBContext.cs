using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Database;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions
        <BookDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Book { get; set; }
    public virtual DbSet<LoanedBook> LoanedBook { get; set; }
   
    public virtual DbSet<FavoriteBooks> FavoriteBooks { get; set; }
    public virtual DbSet<AllLoanedBooks> AllLoanedBooks { get; set; }
}