using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Database;

public class LoginDbContext : DbContext
{
    public LoginDbContext(DbContextOptions
        <LoginDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserData> UserData { get; set; }
}