using System;
using System.Threading.Tasks;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IDbProvider _dbProvider;

        public DashboardRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<object> GetDashboardOverview(int userId)
        {
            return await _dbProvider.ExecuteFirstAsync(
                "dashboard.sp_Dashboard_GetOverview",
                new { UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetActivityFeed(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "dashboard.sp_Dashboard_GetActivityFeed",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetNotifications(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "notification.sp_Notification_GetByUser",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> MarkNotificationAsRead(int notificationId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "notification.sp_Notification_MarkAsRead",
                new { NotificationId = notificationId, UserId = userId, ReadDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> MarkAllNotificationsAsRead(int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "notification.sp_Notification_MarkAllAsRead",
                new { UserId = userId, ReadDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<object> GetUserStatistics(int userId)
        {
            return await _dbProvider.ExecuteFirstAsync(
                "dashboard.sp_Dashboard_GetUserStatistics",
                new { UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetRecentActivity(int userId, int days)
        {
            return await _dbProvider.ExecuteListAsync(
                "dashboard.sp_Dashboard_GetRecentActivity",
                new { UserId = userId, Days = days },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }
    }
}
