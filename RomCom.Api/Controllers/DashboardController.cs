using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Service.Services.Contracts;

namespace RomCom.Api.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get dashboard overview data
        /// </summary>
        /// <returns>Dashboard overview information</returns>
        [HttpGet]
        public async Task<IActionResult> GetOverview()
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetDashboardOverview(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get user's activity feed
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>User's activity feed</returns>
        [HttpGet]
        [Route("activity")]
        public async Task<IActionResult> GetActivityFeed([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetActivityFeed(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get user's notifications
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>User's notifications</returns>
        [HttpGet]
        [Route("notifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetNotifications(userId, page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Mark notification as read
        /// </summary>
        /// <param name="notificationId">Notification ID</param>
        /// <returns>Success status</returns>
        [HttpPut]
        [Route("notifications/{notificationId}/read")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var userId = GetUserId();
            var result = await _dashboardService.MarkNotificationAsRead(notificationId, userId);
            return Ok(result);
        }

        /// <summary>
        /// Mark all notifications as read
        /// </summary>
        /// <returns>Success status</returns>
        [HttpPut]
        [Route("notifications/read-all")]
        public async Task<IActionResult> MarkAllNotificationsAsRead()
        {
            var userId = GetUserId();
            var result = await _dashboardService.MarkAllNotificationsAsRead(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get user's statistics
        /// </summary>
        /// <returns>User statistics</returns>
        [HttpGet]
        [Route("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetUserStatistics(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get recent activity
        /// </summary>
        /// <param name="days">Number of days to look back</param>
        /// <returns>Recent activity data</returns>
        [HttpGet]
        [Route("recent-activity")]
        public async Task<IActionResult> GetRecentActivity([FromQuery] int days = 7)
        {
            var userId = GetUserId();
            var result = await _dashboardService.GetRecentActivity(userId, days);
            return Ok(result);
        }
    }
}
