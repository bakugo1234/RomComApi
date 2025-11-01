using System.Threading.Tasks;
using RomCom.Model.DTOs.Comment.Responses;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface ICommentRepository
    {
        Task<int> CreateComment(CreateCommentDto dto);
        Task<CommentDto> GetCommentById(int commentId, int currentUserId);
        Task<CommentListResult> GetCommentsByPost(int postId, int currentUserId, int pageNumber, int pageSize);
        Task<bool> DeleteComment(int commentId, int userId);
        Task<bool> LikeComment(int commentId, int userId);
        Task<bool> UnlikeComment(int commentId, int userId);
    }
}
