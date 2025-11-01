using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Model.DTOs.Comment.Requests;
using RomCom.Service.Services.Contracts;

namespace RomCom.Api.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Create a new comment on a post
        /// </summary>
        /// <param name="model">Comment creation data</param>
        /// <returns>Created comment information</returns>
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto model)
        {
            var userId = GetUserId();
            var result = await _commentService.CreateComment(userId, model);
            return Ok(result);
        }

        /// <summary>
        /// Get all comments for a specific post
        /// </summary>
        /// <param name="postId">Post ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of comments with pagination</returns>
        [HttpGet]
        [Route("post/{postId}")]
        public async Task<IActionResult> GetCommentsByPost(int postId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var currentUserId = GetUserId();
            var result = await _commentService.GetCommentsByPost(postId, currentUserId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Delete a comment (only comment owner can delete)
        /// </summary>
        /// <param name="commentId">Comment ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var userId = GetUserId();
            var result = await _commentService.DeleteComment(commentId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Like a comment
        /// </summary>
        /// <param name="commentId">Comment ID</param>
        /// <returns>Success status</returns>
        [HttpPost]
        [Route("{commentId}/like")]
        public async Task<IActionResult> LikeComment(int commentId)
        {
            var userId = GetUserId();
            var result = await _commentService.LikeComment(commentId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Remove like from a comment
        /// </summary>
        /// <param name="commentId">Comment ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{commentId}/like")]
        public async Task<IActionResult> UnlikeComment(int commentId)
        {
            var userId = GetUserId();
            var result = await _commentService.UnlikeComment(commentId, userId);
            return Ok(result);
        }
    }
}
