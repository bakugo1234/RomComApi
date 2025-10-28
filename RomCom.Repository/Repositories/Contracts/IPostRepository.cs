using System.Threading.Tasks;
using RomCom.Model.DTOs.Post.Requests;
using RomCom.Model.DTOs.Post.Responses;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IPostRepository
    {
        Task<int> CreatePost(int userId, CreatePostRequestDto model);
        Task<PostDto> GetPost(int postId, int currentUserId);
        Task<object> GetUserPosts(int userId, int currentUserId, int page, int pageSize);
        Task<object> GetUserFeed(int userId, int page, int pageSize);
        Task<object> GetGroupPosts(int groupId, int currentUserId, int page, int pageSize);
        Task<bool> LikePost(int postId, int userId);
        Task<bool> UnlikePost(int postId, int userId);
        Task<bool> DeletePost(int postId, int userId);
        Task<object> GetTrendingPosts(int currentUserId, int page, int pageSize);
    }
}
