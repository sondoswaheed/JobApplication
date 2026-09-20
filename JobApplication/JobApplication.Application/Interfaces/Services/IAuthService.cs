using JobApplication.Application.DTOs.Auth;

namespace JobApplication.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<AuthResponse?> LoginAsync(LoginDto dto);
    }
}