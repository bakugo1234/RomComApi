using System;
using System.Threading.Tasks;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;

        public DashboardService(IDashboardRepository dashboardRepository, IServiceResult successResult, IServiceResult errorResult)
        {
            _dashboardRepository = dashboardRepository;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> GetDashboardOverview(int userId)
        {
            try
            {
                var overview = await _dashboardRepository.GetDashboardOverview(userId);
                _successResponse.ResultData = overview;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get dashboard overview: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetActivityFeed(int userId, int page, int pageSize)
        {
            try
            {
                var activities = await _dashboardRepository.GetActivityFeed(userId, page, pageSize);
                _successResponse.ResultData = activities;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get activity feed: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetNotifications(int userId, int page, int pageSize)
        {
            try
            {
                var notifications = await _dashboardRepository.GetNotifications(userId, page, pageSize);
                _successResponse.ResultData = notifications;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get notifications: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> MarkNotificationAsRead(int notificationId, int userId)
        {
            try
            {
                var success = await _dashboardRepository.MarkNotificationAsRead(notificationId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to mark notification as read";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to mark notification as read: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> MarkAllNotificationsAsRead(int userId)
        {
            try
            {
                var success = await _dashboardRepository.MarkAllNotificationsAsRead(userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to mark all notifications as read";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to mark all notifications as read: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetUserStatistics(int userId)
        {
            try
            {
                var statistics = await _dashboardRepository.GetUserStatistics(userId);
                _successResponse.ResultData = statistics;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get user statistics: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetRecentActivity(int userId, int days)
        {
            try
            {
                var activities = await _dashboardRepository.GetRecentActivity(userId, days);
                _successResponse.ResultData = activities;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get recent activity: {ex.Message}";
                return _errorResponse;
            }
        }
    }
}
