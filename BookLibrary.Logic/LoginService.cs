using BookLibrary.Database;

namespace BookLibrary.Logic;

public class LoginService
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
}