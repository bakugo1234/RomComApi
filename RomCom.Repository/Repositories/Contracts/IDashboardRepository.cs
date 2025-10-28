using System.Threading.Tasks;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IDashboardRepository
    {
        Task<object> GetDashboardOverview(int userId);
        Task<object> GetActivityFeed(int userId, int page, int pageSize);
        Task<object> GetNotifications(int userId, int page, int pageSize);
        Task<bool> MarkNotificationAsRead(int notificationId, int userId);
        Task<bool> MarkAllNotificationsAsRead(int userId);
        Task<object> GetUserStatistics(int userId);
        Task<object> GetRecentActivity(int userId, int days);
    }
}
