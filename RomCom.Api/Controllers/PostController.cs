using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Model.DTOs.Post.Requests;
using RomCom.Model.DTOs.Post.Responses;
using RomCom.Service.Services.Contracts;

namespace RomCom.Api.Controllers
{
    public class PostController : BaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <param name="model">Post creation data</param>
        /// <returns>Created post information</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto model)
        {
            var userId = GetUserId();
            var result = await _postService.CreatePost(userId, model);
            return Ok(result);
        }

        /// <summary>
        /// Get post by ID
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <returns>Post information</returns>
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var currentUserId = GetUserId();
            var result = await _postService.GetPost(postId, currentUserId);
            return Ok(result);
        }

        /// <summary>
        /// Get user's posts
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of user's posts</returns>
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetUserPosts(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var currentUserId = GetUserId();
            var result = await _postService.GetUserPosts(userId, currentUserId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get user's feed (posts from followed users and groups)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>User's feed posts</returns>
        [HttpGet]
        [Route("feed")]
        public async Task<IActionResult> GetUserFeed([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var userId = GetUserId();
            var result = await _postService.GetUserFeed(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get group posts
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of group posts</returns>
        [HttpGet]
        [Route("group/{groupId}")]
        public async Task<IActionResult> GetGroupPosts(int groupId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var currentUserId = GetUserId();
            var result = await _postService.GetGroupPosts(groupId, currentUserId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Like a post
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <returns>Success status</returns>
        [HttpPost]
        [Route("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = GetUserId();
            var result = await _postService.LikePost(postId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Unlike a post
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{postId}/like")]
        public async Task<IActionResult> UnlikePost(int postId)
        {
            var userId = GetUserId();
            var result = await _postService.UnlikePost(postId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Delete a post
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = GetUserId();
            var result = await _postService.DeletePost(postId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get trending posts
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of trending posts</returns>
        [HttpGet]
        [Route("trending")]
        public async Task<IActionResult> GetTrendingPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var currentUserId = GetUserId();
            var result = await _postService.GetTrendingPosts(currentUserId, page, pageSize);
            return Ok(result);
        }
    }
}
