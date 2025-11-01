using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RomCom.Model.DTOs.Auth.Requests;
using RomCom.Model.DTOs.Auth.Responses;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;
        private readonly IAuthRepository _authRepository;

        public LoginService(IConfiguration configuration, 
                          IServiceResult successResult, 
                          IServiceResult errorResult,
                          IAuthRepository authRepository)
        {
            _configuration = configuration;
            _authRepository = authRepository;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> Login(LoginRequestDto credentials)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(credentials.UserName) || string.IsNullOrWhiteSpace(credentials.Password))
                {
                    _errorResponse.Message = "Username and password are required";
                    return _errorResponse;
                }

                // Hash the password for database comparison
                var passwordHash = HashPassword(credentials.Password);
                
                // Validate user against database
                var user = await _authRepository.ValidateUser(credentials.UserName, passwordHash);
                
                if (user == null)
                {
                    _errorResponse.Message = "Invalid username or password";
                    return _errorResponse;
                }

                // Update last login date
                await _authRepository.UpdateLastLogin(user.id, DateTimeOffset.UtcNow);

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                // Store refresh token in database
                await _authRepository.CreateRefreshToken(new CreateRefreshTokenDto
                {
                    UserId = user.id,
                    Token = refreshToken,
                    ExpiresAt = DateTimeOffset.UtcNow.AddDays(30),
                    CreatedDate = DateTimeOffset.UtcNow
                });

                var response = new TokenResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(24),
                    User = user
                };

                _successResponse.ResultData = response;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Login failed: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> RefreshToken(RefreshTokenRequestDto model)
        {
            try
            {
                // 1. Validate Input
                if (string.IsNullOrWhiteSpace(model.RefreshToken))
                {
                    _errorResponse.Message = "Refresh token is required";
                    return _errorResponse;
                }

                // 2. Retrieve Token from Database
                var tokenData = await _authRepository.GetRefreshToken(model.RefreshToken);

                if (tokenData == null)
                {
                    _errorResponse.Message = "Invalid refresh token";
                    return _errorResponse;
                }

                // 3. Validate Token Status
                if (tokenData.IsRevoked)
                {
                    _errorResponse.Message = "Refresh token has been revoked";
                    return _errorResponse;
                }

                if (tokenData.ExpiresAt < DateTimeOffset.UtcNow)
                {
                    _errorResponse.Message = "Refresh token has expired";
                    return _errorResponse;
                }

                // 4. Get User Information
                var user = await _authRepository.GetUserById(tokenData.UserId);

                if (user == null)
                {
                    _errorResponse.Message = "User not found or inactive";
                    return _errorResponse;
                }

                // 5. Rotate Refresh Token (Security Best Practice)
                // Invalidate old refresh token
                await _authRepository.InvalidateRefreshToken(model.RefreshToken);

                // Generate new tokens
                var newJwtToken = GenerateJwtToken(user);
                var newRefreshToken = GenerateRefreshToken();

                // Store new refresh token in database
                await _authRepository.CreateRefreshToken(new CreateRefreshTokenDto
                {
                    UserId = user.id,
                    Token = newRefreshToken,
                    ExpiresAt = DateTimeOffset.UtcNow.AddDays(30),
                    CreatedDate = DateTimeOffset.UtcNow
                });

                // 6. Return Success Response
                var response = new TokenResponseDto
                {
                    Token = newJwtToken,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(24),
                    User = user
                };

                _successResponse.ResultData = response;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Refresh token failed: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> ChangePassword(ChangePasswordRequestDto model, string userName)
        {
            try
            {
                // 1. Validate Input
                if (string.IsNullOrWhiteSpace(userName))
                {
                    _errorResponse.Message = "User name is required";
                    return _errorResponse;
                }

                if (string.IsNullOrWhiteSpace(model.OldPassword))
                {
                    _errorResponse.Message = "Current password is required";
                    return _errorResponse;
                }

                if (string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    _errorResponse.Message = "New password is required";
                    return _errorResponse;
                }

                if (model.NewPassword.Length < 6)
                {
                    _errorResponse.Message = "Password must be at least 6 characters";
                    return _errorResponse;
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    _errorResponse.Message = "Passwords do not match";
                    return _errorResponse;
                }

                if (model.OldPassword == model.NewPassword)
                {
                    _errorResponse.Message = "New password must be different from current password";
                    return _errorResponse;
                }

                // 2. Get User
                var user = await _authRepository.GetUserByUserName(userName);

                if (user == null)
                {
                    _errorResponse.Message = "User not found";
                    return _errorResponse;
                }

                // 3. Verify Old Password
                var hashedOldPassword = HashPassword(model.OldPassword);
                var storedPasswordHash = await _authRepository.GetPasswordHash(user.id);

                if (hashedOldPassword != storedPasswordHash)
                {
                    _errorResponse.Message = "Current password is incorrect";
                    return _errorResponse;
                }

                // 4. Update Password
                var hashedNewPassword = HashPassword(model.NewPassword);
                var updateSuccess = await _authRepository.UpdatePassword(user.id, hashedNewPassword);

                if (!updateSuccess)
                {
                    _errorResponse.Message = "Failed to update password";
                    return _errorResponse;
                }

                // 5. Invalidate All Refresh Tokens (Security Best Practice)
                // This forces user to re-login on all devices after password change
                await _authRepository.InvalidateAllUserRefreshTokens(user.id);

                // 6. Return Success Response
                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Change password failed: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> Logout(int userId)
        {
            try
            {
                // 1. Validate Input
                if (userId <= 0)
                {
                    _errorResponse.Message = "Invalid user ID";
                    return _errorResponse;
                }

                // 2. Invalidate All User Refresh Tokens
                // This revokes all refresh tokens for the user (logs them out from all devices)
                await _authRepository.InvalidateAllUserRefreshTokens(userId);

                // 3. Return Success Response
                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Logout failed: {ex.Message}";
                return _errorResponse;
            }
        }

        private string HashPassword(string password)
        {
            // Simple hash implementation - in production, use BCrypt or similar
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private string GenerateJwtToken(AuthDto user)
        {
            var jwtSecret = _configuration["AppSettings:JWTSecret"] ?? "your-secret-key-min-32-characters-long-for-security";
            var tokenExpiryHours = int.Parse(_configuration["AppSettings:TokenExpiryHours"] ?? "24");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.userName),
                new Claim(ClaimTypes.Email, user.email ?? ""),
                new Claim(ClaimTypes.Role, user.roleName ?? "User"),
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user))
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTimeOffset.UtcNow.AddHours(tokenExpiryHours).DateTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

