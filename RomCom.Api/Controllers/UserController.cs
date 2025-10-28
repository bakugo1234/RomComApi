using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Model.DTOs.User.Requests;
using RomCom.Model.DTOs.User.Responses;
using RomCom.Service.Services.Contracts;

namespace RomCom.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get current user's profile
        /// </summary>
        /// <returns>User profile information</returns>
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var result = await _userService.GetUserProfile(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get user profile by ID
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User profile information</returns>
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var result = await _userService.GetUserProfile(userId);
            return Ok(result);
        }

        /// <summary>
        /// Update current user's profile
        /// </summary>
        /// <param name="model">Profile update data</param>
        /// <returns>Updated profile information</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequestDto model)
        {
            var userId = GetUserId();
            var result = await _userService.UpdateUserProfile(userId, model);
            return Ok(result);
        }

        /// <summary>
        /// Create a new user account
        /// </summary>
        /// <param name="model">User creation data</param>
        /// <returns>Created user information</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto model)
        {
            var result = await _userService.CreateUser(model);
            return Ok(result);
        }

        /// <summary>
        /// Search users by username or name
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of matching users</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _userService.SearchUsers(searchTerm, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get user's followers
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of followers</returns>
        [HttpGet]
        [Route("{userId}/followers")]
        public async Task<IActionResult> GetFollowers(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _userService.GetFollowers(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get user's following list
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of following users</returns>
        [HttpGet]
        [Route("{userId}/following")]
        public async Task<IActionResult> GetFollowing(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _userService.GetFollowing(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Follow a user
        /// </summary>
        /// <param name="userId">User ID to follow</param>
        /// <returns>Success status</returns>
        [HttpPost]
        [Route("{userId}/follow")]
        public async Task<IActionResult> FollowUser(int userId)
        {
            var currentUserId = GetUserId();
            var result = await _userService.FollowUser(currentUserId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Unfollow a user
        /// </summary>
        /// <param name="userId">User ID to unfollow</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{userId}/follow")]
        public async Task<IActionResult> UnfollowUser(int userId)
        {
            var currentUserId = GetUserId();
            var result = await _userService.UnfollowUser(currentUserId, userId);
            return Ok(result);
        }
    }
}
