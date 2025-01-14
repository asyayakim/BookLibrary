using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Database;

public class LoginRepository
{
    private readonly LoginDbContext _context;

    public LoginRepository(LoginDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserData>> GetAllAsync()
    {
        return await _context.UserData.ToListAsync();
    }

    public async Task<UserData?> GetByIdAsync(int id)
    {
        return await _context.UserData.FindAsync(id);
    }

    public async Task AddAsync(UserData userData)
    {
        var existingUser = await _context.UserData.FirstOrDefaultAsync(b => b.UserName == userData.UserName);
        if (existingUser != null)
        {
            Console.WriteLine($"Skipping duplicate book: {userData.UserName}");
            return;
        }

        _context.UserData.Add(userData);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var userData = await _context.UserData.FindAsync(id);
        if (userData != null)
        {
            _context.UserData.Remove(userData);
            await _context.SaveChangesAsync();
        }
    }

    public UserData GetUsersByData(string requestUserName, string requestPassword)
    {
        return _context.UserData.FirstOrDefault(b => b.UserName == requestUserName &&
                                                     b.Password == requestPassword)!;
    }
}