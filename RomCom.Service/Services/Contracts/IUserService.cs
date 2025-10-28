using System.Threading.Tasks;
using RomCom.Model.DTOs.User.Requests;
using RomCom.Model.DTOs.User.Responses;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface IUserService
    {
        Task<IServiceResult> GetUserProfile(int userId);
        Task<IServiceResult> UpdateUserProfile(int userId, UpdateUserProfileRequestDto model);
        Task<IServiceResult> CreateUser(CreateUserRequestDto model);
        Task<IServiceResult> SearchUsers(string searchTerm, int page, int pageSize);
        Task<IServiceResult> GetFollowers(int userId, int page, int pageSize);
        Task<IServiceResult> GetFollowing(int userId, int page, int pageSize);
        Task<IServiceResult> FollowUser(int currentUserId, int targetUserId);
        Task<IServiceResult> UnfollowUser(int currentUserId, int targetUserId);
    }
}
