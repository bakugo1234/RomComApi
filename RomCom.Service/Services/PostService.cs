using System;
using System.Threading.Tasks;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Post.Requests;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;

        public PostService(IPostRepository postRepository, IServiceResult successResult, IServiceResult errorResult)
        {
            _postRepository = postRepository;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> CreatePost(int userId, CreatePostRequestDto model)
        {
            try
            {
                var postId = await _postRepository.CreatePost(userId, model);
                if (postId <= 0)
                {
                    _errorResponse.Message = "Failed to create post";
                    return _errorResponse;
                }

                var createdPost = await _postRepository.GetPost(postId, userId);
                _successResponse.ResultData = createdPost;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to create post: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetPost(int postId, int currentUserId)
        {
            try
            {
                var post = await _postRepository.GetPost(postId, currentUserId);
                if (post == null)
                {
                    _errorResponse.Message = "Post not found";
                    return _errorResponse;
                }

                _successResponse.ResultData = post;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get post: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetUserPosts(int userId, int currentUserId, int page, int pageSize)
        {
            try
            {
                var posts = await _postRepository.GetUserPosts(userId, currentUserId, page, pageSize);
                _successResponse.ResultData = posts;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get user posts: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetUserFeed(int userId, int page, int pageSize)
        {
            try
            {
                var posts = await _postRepository.GetUserFeed(userId, page, pageSize);
                _successResponse.ResultData = posts;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get user feed: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetGroupPosts(int groupId, int currentUserId, int page, int pageSize)
        {
            try
            {
                var posts = await _postRepository.GetGroupPosts(groupId, currentUserId, page, pageSize);
                _successResponse.ResultData = posts;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get group posts: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> LikePost(int postId, int userId)
        {
            try
            {
                var success = await _postRepository.LikePost(postId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to like post";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to like post: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> UnlikePost(int postId, int userId)
        {
            try
            {
                var success = await _postRepository.UnlikePost(postId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to unlike post";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to unlike post: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> DeletePost(int postId, int userId)
        {
            try
            {
                var success = await _postRepository.DeletePost(postId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to delete post or post not found";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to delete post: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetTrendingPosts(int currentUserId, int page, int pageSize)
        {
            try
            {
                var posts = await _postRepository.GetTrendingPosts(currentUserId, page, pageSize);
                _successResponse.ResultData = posts;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get trending posts: {ex.Message}";
                return _errorResponse;
            }
        }
    }
}
