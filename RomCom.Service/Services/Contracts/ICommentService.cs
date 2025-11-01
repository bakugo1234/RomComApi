using System.Threading.Tasks;
using RomCom.Model.DTOs.Comment.Requests;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface ICommentService
    {
        Task<IServiceResult> CreateComment(int userId, CreateCommentRequestDto model);
        Task<IServiceResult> GetCommentsByPost(int postId, int currentUserId, int pageNumber, int pageSize);
        Task<IServiceResult> DeleteComment(int commentId, int userId);
        Task<IServiceResult> LikeComment(int commentId, int userId);
        Task<IServiceResult> UnlikeComment(int commentId, int userId);
    }
}
