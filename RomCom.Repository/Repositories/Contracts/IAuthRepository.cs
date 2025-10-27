using System;
using System.Threading.Tasks;
using RomCom.Model.ViewModels;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<AuthViewModel> ValidateUser(string userName, string passwordHash);
        Task<AuthViewModel> GetUserById(int userId);
        Task<AuthViewModel> GetUserByUserName(string userName);
        Task<int> CreateUser(CreateUserDto dto);
        Task<bool> UpdatePassword(int userId, string passwordHash);
        Task<bool> UpdateLastLogin(int userId, DateTime lastLoginDate);
        Task<string> GetPasswordHash(int userId);
        
        // Refresh Token Methods
        Task<int> CreateRefreshToken(CreateRefreshTokenDto dto);
        Task<RefreshTokenDto> GetRefreshToken(string refreshToken);
        Task<bool> InvalidateRefreshToken(string refreshToken);
        Task<bool> InvalidateAllUserRefreshTokens(int userId);
    }
}

