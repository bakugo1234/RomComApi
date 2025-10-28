using System;
using System.Threading.Tasks;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.User.Requests;
using RomCom.Model.DTOs.User.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class UserRepository : IUserRepository
    {
        private readonly IDbProvider _dbProvider;

        public UserRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<UserProfileDto> GetUserProfile(int userId)
        {
            return await _dbProvider.ExecuteFirstAsync<UserProfileDto>(
                "user.sp_User_GetProfile",
                new { UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> UpdateUserProfile(int userId, UpdateUserProfileRequestDto model)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "user.sp_User_UpdateProfile",
                new
                {
                    UserId = userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePicture = model.ProfilePicture,
                    Bio = model.Bio,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Location = model.Location,
                    Website = model.Website,
                    CoverImage = model.CoverImage,
                    IsPrivate = model.IsPrivate,
                    ModifiedDate = DateTimeOffset.UtcNow
                },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> CreateUserProfile(int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "user.sp_User_CreateProfile",
                new { UserId = userId, CreatedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<object> SearchUsers(string searchTerm, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "user.sp_User_SearchUsers",
                new { SearchTerm = searchTerm, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetFollowers(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "friendship.sp_Friendship_GetFollowers",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetFollowing(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "friendship.sp_Friendship_GetFollowing",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> FollowUser(int currentUserId, int targetUserId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "friendship.sp_Friendship_SendRequest",
                new { UserId = currentUserId, FriendId = targetUserId, RequestedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> UnfollowUser(int currentUserId, int targetUserId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "friendship.sp_Friendship_Unfollow",
                new { UserId = currentUserId, TargetUserId = targetUserId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }
    }
}
