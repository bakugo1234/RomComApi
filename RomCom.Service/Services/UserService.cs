using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.User.Requests;
using RomCom.Model.DTOs.User.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;

        public UserService(IUserRepository userRepository, IAuthRepository authRepository, IServiceResult successResult, IServiceResult errorResult)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> GetUserProfile(int userId)
        {
            try
            {
                var profile = await _userRepository.GetUserProfile(userId);
                if (profile == null)
                {
                    _errorResponse.Message = "User profile not found";
                    return _errorResponse;
                }

                _successResponse.ResultData = profile;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get user profile: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> UpdateUserProfile(int userId, UpdateUserProfileRequestDto model)
        {
            try
            {
                var success = await _userRepository.UpdateUserProfile(userId, model);
                if (!success)
                {
                    _errorResponse.Message = "Failed to update user profile";
                    return _errorResponse;
                }

                var updatedProfile = await _userRepository.GetUserProfile(userId);
                _successResponse.ResultData = updatedProfile;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to update user profile: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> CreateUser(CreateUserRequestDto model)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _authRepository.GetUserByUserName(model.UserName);
                if (existingUser != null)
                {
                    _errorResponse.Message = "Username already exists";
                    return _errorResponse;
                }

                // Hash password
                var passwordHash = HashPassword(model.Password);

                // Create user in auth database
                var createUserDto = new CreateUserDto
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePicture = model.ProfilePicture,
                    RoleId = 2 // Default user role
                };

                var userId = await _authRepository.CreateUser(createUserDto);
                if (userId <= 0)
                {
                    _errorResponse.Message = "Failed to create user";
                    return _errorResponse;
                }

                // Create user profile
                var profileCreated = await _userRepository.CreateUserProfile(userId);
                if (!profileCreated)
                {
                    _errorResponse.Message = "Failed to create user profile";
                    return _errorResponse;
                }

                var newProfile = await _userRepository.GetUserProfile(userId);
                _successResponse.ResultData = newProfile;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to create user: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> SearchUsers(string searchTerm, int page, int pageSize)
        {
            try
            {
                var users = await _userRepository.SearchUsers(searchTerm, page, pageSize);
                _successResponse.ResultData = users;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to search users: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetFollowers(int userId, int page, int pageSize)
        {
            try
            {
                var followers = await _userRepository.GetFollowers(userId, page, pageSize);
                _successResponse.ResultData = followers;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get followers: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetFollowing(int userId, int page, int pageSize)
        {
            try
            {
                var following = await _userRepository.GetFollowing(userId, page, pageSize);
                _successResponse.ResultData = following;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get following: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> FollowUser(int currentUserId, int targetUserId)
        {
            try
            {
                if (currentUserId == targetUserId)
                {
                    _errorResponse.Message = "Cannot follow yourself";
                    return _errorResponse;
                }

                var success = await _userRepository.FollowUser(currentUserId, targetUserId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to follow user";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to follow user: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> UnfollowUser(int currentUserId, int targetUserId)
        {
            try
            {
                var success = await _userRepository.UnfollowUser(currentUserId, targetUserId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to unfollow user";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to unfollow user: {ex.Message}";
                return _errorResponse;
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
