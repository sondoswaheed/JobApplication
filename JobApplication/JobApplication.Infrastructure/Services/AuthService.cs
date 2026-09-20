using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return false;

            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password
            );

            return result.Succeeded;
        }

        public async Task<AuthResponse?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var passwordValid = await _userManager.CheckPasswordAsync(
                user,
                dto.Password
            );

            if (!passwordValid)
                return null;

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email!
            );

            return new AuthResponse
            {
                Token = token
            };
        }
    }
}