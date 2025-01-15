using BookLibrary.Database;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Logic;

public class LoginService : ILoginService
{
    private readonly LoginRepository _loginRepository;

    public LoginService(LoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<List<UserData>> GetUsersDataAsync()
    {
        return await _loginRepository.GetAllAsync();
    }

    public async Task<UserData?> GetUserDataByIdAsync(int id)
    {
        return await _loginRepository.GetByIdAsync(id);
    }

    public async Task AddUserDataAsync(UserData userData)
    {
        await _loginRepository.AddAsync(userData);
    }

    public async Task DeleteUserDataAsync(int id)
    {
        await _loginRepository.DeleteAsync(id);
    }

    public UserData GetUsersDataAsync(string requestUserName, string requestPassword)
    {
        return _loginRepository.GetUsersByData(requestUserName, requestPassword);
    }

    public async Task<UserData> AddUserDataAsync(string requestUserName, string requestPassword)
    {
        var userData = new UserData
        {
            UserName = requestUserName,
            Password = requestPassword,
            Role = "User"
        };

        await _loginRepository.AddAsync(userData);
        return userData;
    }
}