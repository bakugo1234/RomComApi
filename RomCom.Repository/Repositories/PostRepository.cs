using System;
using System.Threading.Tasks;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Post.Requests;
using RomCom.Model.DTOs.Post.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class PostRepository : IPostRepository
    {
        private readonly IDbProvider _dbProvider;

        public PostRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<int> CreatePost(int userId, CreatePostRequestDto model)
        {
            return await _dbProvider.ExecuteScalarAsync<int>(
                "post.sp_Post_Create",
                new
                {
                    UserId = userId,
                    Content = model.Content,
                    MediaType = model.MediaType,
                    MediaUrl = model.MediaUrl,
                    GroupId = model.GroupId,
                    CreatedDate = DateTimeOffset.UtcNow
                },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<PostDto> GetPost(int postId, int currentUserId)
        {
            return await _dbProvider.ExecuteFirstAsync<PostDto>(
                "post.sp_Post_GetById",
                new { PostId = postId, CurrentUserId = currentUserId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetUserPosts(int userId, int currentUserId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "post.sp_Post_GetByUser",
                new { UserId = userId, CurrentUserId = currentUserId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetUserFeed(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "feed.sp_Feed_GetUserFeed",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetGroupPosts(int groupId, int currentUserId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "post.sp_Post_GetByGroup",
                new { GroupId = groupId, CurrentUserId = currentUserId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> LikePost(int postId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "post.sp_Post_Like",
                new { PostId = postId, UserId = userId, CreatedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> UnlikePost(int postId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "post.sp_Post_Unlike",
                new { PostId = postId, UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> DeletePost(int postId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "post.sp_Post_Delete",
                new { PostId = postId, UserId = userId, DeletedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<object> GetTrendingPosts(int currentUserId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "post.sp_Post_GetTrending",
                new { CurrentUserId = currentUserId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }
    }
}
