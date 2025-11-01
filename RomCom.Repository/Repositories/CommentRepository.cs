using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Comment.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;
using RomCom.Repository.Setup.DTOs;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbProvider _dbProvider;
        private readonly IConnectionProvider _connectionProvider;

        public CommentRepository(IDbProvider dbProvider, IConnectionProvider connectionProvider)
        {
            _dbProvider = dbProvider;
            _connectionProvider = connectionProvider;
        }

        public async Task<int> CreateComment(CreateCommentDto dto)
        {
            return await _dbProvider.ExecuteScalarAsync<int>(
                "comment.sp_Comment_Create",
                dto,
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<CommentDto> GetCommentById(int commentId, int currentUserId)
        {
            return await _dbProvider.ExecuteFirstAsync<CommentDto>(
                "comment.sp_Comment_GetById",
                new { CommentId = commentId, CurrentUserId = currentUserId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<CommentListResult> GetCommentsByPost(int postId, int currentUserId, int pageNumber, int pageSize)
        {
            // Use QueryMultiple to read two result sets
            using var conn = _connectionProvider.GetOpenDbConnection(Region.Main);
            using var multi = await conn.QueryMultipleAsync(
                "comment.sp_Comment_GetByPost",
                new { PostId = postId, CurrentUserId = currentUserId, PageNumber = pageNumber, PageSize = pageSize },
                commandType: System.Data.CommandType.StoredProcedure);

            var comments = (await multi.ReadAsync<CommentDto>()).ToList();
            var totalCount = await multi.ReadFirstOrDefaultAsync<int>();

            return new CommentListResult
            {
                Comments = comments,
                TotalCount = totalCount
            };
        }

        public async Task<bool> DeleteComment(int commentId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "comment.sp_Comment_Delete",
                new { CommentId = commentId, UserId = userId, DeletedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> LikeComment(int commentId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "comment.sp_CommentLike_Add",
                new { CommentId = commentId, UserId = userId, CreatedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> UnlikeComment(int commentId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "comment.sp_CommentLike_Remove",
                new { CommentId = commentId, UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }
    }
}
