using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Model.DTOs.Group.Requests;
using RomCom.Model.DTOs.Group.Responses;
using RomCom.Service.Services.Contracts;

namespace RomCom.Api.Controllers
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <param name="model">Group creation data</param>
        /// <returns>Created group information</returns>
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequestDto model)
        {
            var userId = GetUserId();
            var result = await _groupService.CreateGroup(userId, model);
            return Ok(result);
        }

        /// <summary>
        /// Get group by ID
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Group information</returns>
        [HttpGet]
        [Route("{groupId}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var currentUserId = GetUserId();
            var result = await _groupService.GetGroup(groupId, currentUserId);
            return Ok(result);
        }

        /// <summary>
        /// Get user's groups
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of user's groups</returns>
        [HttpGet]
        [Route("my-groups")]
        public async Task<IActionResult> GetMyGroups([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var userId = GetUserId();
            var result = await _groupService.GetUserGroups(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Search groups
        /// </summary>
        /// <param name="searchTerm">Search term</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of matching groups</returns>
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchGroups([FromQuery] string searchTerm, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var currentUserId = GetUserId();
            var result = await _groupService.SearchGroups(searchTerm, currentUserId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Join a group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Success status</returns>
        [HttpPost]
        [Route("{groupId}/join")]
        public async Task<IActionResult> JoinGroup(int groupId)
        {
            var userId = GetUserId();
            var result = await _groupService.JoinGroup(groupId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Leave a group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{groupId}/leave")]
        public async Task<IActionResult> LeaveGroup(int groupId)
        {
            var userId = GetUserId();
            var result = await _groupService.LeaveGroup(groupId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get group members
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of group members</returns>
        [HttpGet]
        [Route("{groupId}/members")]
        public async Task<IActionResult> GetGroupMembers(int groupId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _groupService.GetGroupMembers(groupId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Update group information
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="model">Group update data</param>
        /// <returns>Updated group information</returns>
        [HttpPut]
        [Route("{groupId}")]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody] CreateGroupRequestDto model)
        {
            var userId = GetUserId();
            var result = await _groupService.UpdateGroup(groupId, userId, model);
            return Ok(result);
        }

        /// <summary>
        /// Delete a group
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <returns>Success status</returns>
        [HttpDelete]
        [Route("{groupId}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            var userId = GetUserId();
            var result = await _groupService.DeleteGroup(groupId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get trending groups
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of trending groups</returns>
        [HttpGet]
        [Route("trending")]
        public async Task<IActionResult> GetTrendingGroups([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var currentUserId = GetUserId();
            var result = await _groupService.GetTrendingGroups(currentUserId, page, pageSize);
            return Ok(result);
        }
    }
}
