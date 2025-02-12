using BookLibrary.Database;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Logic;

public interface ILoginService
{
    Task<List<UserData>> GetUsersDataAsync();
    Task<UserData?> GetUserDataByIdAsync(int id);
    Task AddUserDataAsync(UserData userData);
    Task DeleteUserDataAsync(int id);
    UserData GetUsersDataAsync(string requestUserName, string requestPassword);
    Task<UserData> AddUserDataAsync(string requestUserName, string requestPassword);
    Task<List<UserData>> GetAllUsersDataAsync();
    Task ChangeUserDataAsync(string requestUserName, string requestPassword, int requestId);

    Task<Task<UserData?>> GetUserDataAsync(int requestId);
}