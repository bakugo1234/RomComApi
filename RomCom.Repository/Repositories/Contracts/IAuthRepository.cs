using System;
using System.Threading.Tasks;
using RomCom.Model.DTOs.Auth.Responses;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<AuthDto> ValidateUser(string userName, string passwordHash);
        Task<AuthDto> GetUserById(int userId);
        Task<AuthDto> GetUserByUserName(string userName);
        Task<int> CreateUser(CreateUserDto dto);
        Task<bool> UpdatePassword(int userId, string passwordHash);
        Task<bool> UpdateLastLogin(int userId, DateTimeOffset lastLoginDate);
        Task<string> GetPasswordHash(int userId);
        
        // Refresh Token Methods
        Task<int> CreateRefreshToken(CreateRefreshTokenDto dto);
        Task<RefreshTokenDto> GetRefreshToken(string refreshToken);
        Task<bool> InvalidateRefreshToken(string refreshToken);
        Task<bool> InvalidateAllUserRefreshTokens(int userId);
    }
}

