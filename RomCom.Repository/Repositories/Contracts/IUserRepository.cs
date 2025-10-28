using System.Threading.Tasks;
using RomCom.Model.DTOs.User.Requests;
using RomCom.Model.DTOs.User.Responses;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<UserProfileDto> GetUserProfile(int userId);
        Task<bool> UpdateUserProfile(int userId, UpdateUserProfileRequestDto model);
        Task<bool> CreateUserProfile(int userId);
        Task<object> SearchUsers(string searchTerm, int page, int pageSize);
        Task<object> GetFollowers(int userId, int page, int pageSize);
        Task<object> GetFollowing(int userId, int page, int pageSize);
        Task<bool> FollowUser(int currentUserId, int targetUserId);
        Task<bool> UnfollowUser(int currentUserId, int targetUserId);
    }
}
