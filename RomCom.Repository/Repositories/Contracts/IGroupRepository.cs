using System.Threading.Tasks;
using RomCom.Model.DTOs.Group.Requests;
using RomCom.Model.DTOs.Group.Responses;

namespace RomCom.Repository.Repositories.Contracts
{
    public interface IGroupRepository
    {
        Task<int> CreateGroup(int userId, CreateGroupRequestDto model);
        Task<GroupDto> GetGroup(int groupId, int currentUserId);
        Task<object> GetUserGroups(int userId, int page, int pageSize);
        Task<object> SearchGroups(string searchTerm, int currentUserId, int page, int pageSize);
        Task<bool> JoinGroup(int groupId, int userId);
        Task<bool> LeaveGroup(int groupId, int userId);
        Task<object> GetGroupMembers(int groupId, int page, int pageSize);
        Task<bool> UpdateGroup(int groupId, int userId, CreateGroupRequestDto model);
        Task<bool> DeleteGroup(int groupId, int userId);
        Task<object> GetTrendingGroups(int currentUserId, int page, int pageSize);
    }
}
