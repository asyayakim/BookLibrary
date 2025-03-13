using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookLibrary.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

    public Task<List<UserData>> GetAllUsersDataAsync()
    {
        return _loginRepository.GetAllUserData();
    }

    public Task ChangeUserDataAsync(string requestUserName, string requestPassword, int requestId)
    {
        return _loginRepository.ChangeUserData(requestUserName, requestPassword, requestId);
    }

    public Task<Task<UserData?>> GetUserDataAsync(int requestId)
    {
        return Task.FromResult(_loginRepository.GetByIdAsync(requestId));
    }

    public string GenerateJwtToken(UserData user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A576E5A7234753777217A25432A462D4A614E645267556B5870327335763879"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        var token = new JwtSecurityToken(
            issuer: "jobtracker-api",
            audience: "obtracker-client",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}