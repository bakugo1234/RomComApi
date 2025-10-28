using System.Threading.Tasks;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface IDashboardService
    {
        Task<IServiceResult> GetDashboardOverview(int userId);
        Task<IServiceResult> GetActivityFeed(int userId, int page, int pageSize);
        Task<IServiceResult> GetNotifications(int userId, int page, int pageSize);
        Task<IServiceResult> MarkNotificationAsRead(int notificationId, int userId);
        Task<IServiceResult> MarkAllNotificationsAsRead(int userId);
        Task<IServiceResult> GetUserStatistics(int userId);
        Task<IServiceResult> GetRecentActivity(int userId, int days);
    }
}
