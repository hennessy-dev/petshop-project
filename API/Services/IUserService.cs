using API.DTOs;

namespace API.Services;
public interface IUserService
{
    Task<string> RegisterAsync(RegisterDto model);
    Task<DataUserDto> GetTokenAsync(LoginDto login);
    Task<string> AddRoleAsync(AddRoleDto model);
    Task<DataUserDto> RefreshTokenAsync(string RefreshToken);
}