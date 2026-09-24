using JobApplication.Application.DTOs.Auth;
using JobApplication.Application.Interfaces;
using JobApplication.Application.Interfaces.Repositories;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;
        private readonly ICandidateRepository _candidateRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            JwtService jwtService,
            ICandidateRepository candidateRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _candidateRepository = candidateRepository;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            // 1. Check email
            var existingUser =
                await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
                return false;

            // 2. Validate role
            if (dto.Role != "Recruiter" &&
                dto.Role != "Candidate")
            {
                return false;
            }

            // 3. Validate Candidate data
            if (dto.Role == "Candidate")
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return false;

                if (string.IsNullOrWhiteSpace(dto.CVUrl))
                    return false;
            }

            // 4. Create Identity User
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password
            );

            if (!result.Succeeded)
                return false;

            // 5. Add Role
            var roleResult = await _userManager.AddToRoleAsync(
                user,
                dto.Role
            );

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return false;
            }

            // 6. Create Candidate Profile
            if (dto.Role == "Candidate")
            {
                var candidate = new Candidate
                {
                    UserId = user.Id,
                    Name = dto.Name!.Trim(),
                    Email = dto.Email,
                    CVUrl = dto.CVUrl!.Trim()
                };

                await _candidateRepository.AddAsync(candidate);
            }

            return true;
        }

        public async Task<AuthResponse?> LoginAsync(LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var passwordValid =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password
                );

            if (!passwordValid)
                return null;

            var roles =
                await _userManager.GetRolesAsync(user);

            var token =
                _jwtService.GenerateToken(
                    user.Id,
                    user.Email!,
                    roles
                );

            return new AuthResponse
            {
                Token = token
            };
        }
    }
}