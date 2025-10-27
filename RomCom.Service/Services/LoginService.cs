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
                // TODO: Implement refresh token validation logic
                // Validate the refresh token from database
                
                _errorResponse.Message = "Refresh token implementation pending";
                return _errorResponse;
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
                // TODO: Implement password change logic
                // Validate old password and update with new password
                
                _errorResponse.Message = "Change password implementation pending";
                return _errorResponse;
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
                // TODO: Implement logout logic
                // Invalidate refresh tokens for the user
                
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

