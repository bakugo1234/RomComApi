using System.Threading.Tasks;
using RomCom.Model.DTOs.Post.Requests;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface IPostService
    {
        Task<IServiceResult> CreatePost(int userId, CreatePostRequestDto model);
        Task<IServiceResult> GetPost(int postId, int currentUserId);
        Task<IServiceResult> GetUserPosts(int userId, int currentUserId, int page, int pageSize);
        Task<IServiceResult> GetUserFeed(int userId, int page, int pageSize);
        Task<IServiceResult> GetGroupPosts(int groupId, int currentUserId, int page, int pageSize);
        Task<IServiceResult> LikePost(int postId, int userId);
        Task<IServiceResult> UnlikePost(int postId, int userId);
        Task<IServiceResult> DeletePost(int postId, int userId);
        Task<IServiceResult> GetTrendingPosts(int currentUserId, int page, int pageSize);
    }
}
