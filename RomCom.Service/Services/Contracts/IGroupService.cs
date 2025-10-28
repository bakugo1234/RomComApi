using System.Threading.Tasks;
using RomCom.Model.DTOs.Group.Requests;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface IGroupService
    {
        Task<IServiceResult> CreateGroup(int userId, CreateGroupRequestDto model);
        Task<IServiceResult> GetGroup(int groupId, int currentUserId);
        Task<IServiceResult> GetUserGroups(int userId, int page, int pageSize);
        Task<IServiceResult> SearchGroups(string searchTerm, int currentUserId, int page, int pageSize);
        Task<IServiceResult> JoinGroup(int groupId, int userId);
        Task<IServiceResult> LeaveGroup(int groupId, int userId);
        Task<IServiceResult> GetGroupMembers(int groupId, int page, int pageSize);
        Task<IServiceResult> UpdateGroup(int groupId, int userId, CreateGroupRequestDto model);
        Task<IServiceResult> DeleteGroup(int groupId, int userId);
        Task<IServiceResult> GetTrendingGroups(int currentUserId, int page, int pageSize);
    }
}
