using BookLibrary.Database;

namespace BookLibrary.Logic;

public interface ILoginService
{
    Task<List<UserData>> GetUsersDataAsync();
    Task<UserData?> GetUserDataByIdAsync(int id);
    Task AddUserDataAsync(UserData userData);
    Task DeleteUserDataAsync(int id);
    UserData GetUsersDataAsync(string requestUserName, string requestPassword);
}