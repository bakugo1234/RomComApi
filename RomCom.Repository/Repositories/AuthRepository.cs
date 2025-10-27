using System;
using System.Threading.Tasks;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.ViewModels;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbProvider _dbProvider;

        public AuthRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<AuthViewModel> ValidateUser(string userName, string passwordHash)
        {
            return await _dbProvider.ExecuteFirstAsync<AuthViewModel>(
                "auth.sp_Auth_ValidateUser",
                new { UserName = userName, PasswordHash = passwordHash },
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<AuthViewModel> GetUserById(int userId)
        {
            return await _dbProvider.ExecuteFirstAsync<AuthViewModel>(
                "auth.sp_Auth_GetUserById",
                new { UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<AuthViewModel> GetUserByUserName(string userName)
        {
            return await _dbProvider.ExecuteFirstAsync<AuthViewModel>(
                "auth.sp_Auth_GetUserByUserName",
                new { UserName = userName },
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<int> CreateUser(CreateUserDto dto)
        {
            return await _dbProvider.ExecuteScalarAsync<int>(
                "auth.sp_Auth_CreateUser",
                dto,
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<bool> UpdatePassword(int userId, string passwordHash)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "auth.sp_Auth_UpdatePassword",
                new { 
                    UserId = userId, 
                    PasswordHash = passwordHash,
                    ModifiedDate = DateTimeOffset.UtcNow 
                },
                System.Data.CommandType.StoredProcedure,
                Region.Default);

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateLastLogin(int userId, DateTimeOffset lastLoginDate)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "auth.sp_Auth_UpdateLastLogin",
                new { UserId = userId, LastLoginDate = lastLoginDate },
                System.Data.CommandType.StoredProcedure,
                Region.Default);

            return rowsAffected > 0;
        }

        public async Task<string> GetPasswordHash(int userId)
        {
            return await _dbProvider.ExecuteScalarAsync<string>(
                "auth.sp_Auth_GetPasswordHash",
                new { UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        #region Refresh Token Methods

        public async Task<int> CreateRefreshToken(CreateRefreshTokenDto dto)
        {
            return await _dbProvider.ExecuteScalarAsync<int>(
                "auth.sp_Auth_CreateRefreshToken",
                dto,
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<RefreshTokenDto> GetRefreshToken(string refreshToken)
        {
            return await _dbProvider.ExecuteFirstAsync<RefreshTokenDto>(
                "auth.sp_Auth_GetRefreshToken",
                new { Token = refreshToken },
                System.Data.CommandType.StoredProcedure,
                Region.Default);
        }

        public async Task<bool> InvalidateRefreshToken(string refreshToken)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "auth.sp_Auth_InvalidateRefreshToken",
                new { Token = refreshToken, RevokedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Default);

            return rowsAffected > 0;
        }

        public async Task<bool> InvalidateAllUserRefreshTokens(int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "auth.sp_Auth_InvalidateAllUserRefreshTokens",
                new { UserId = userId, RevokedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Default);

            return rowsAffected > 0;
        }

        #endregion
    }
}

