using System;
using System.Threading.Tasks;
using RomCom.Common.Enums;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Group.Requests;
using RomCom.Model.DTOs.Group.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Repository.Repositories
{
    [ScopedService]
    public class GroupRepository : IGroupRepository
    {
        private readonly IDbProvider _dbProvider;

        public GroupRepository(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<int> CreateGroup(int userId, CreateGroupRequestDto model)
        {
            return await _dbProvider.ExecuteScalarAsync<int>(
                "group.sp_Group_Create",
                new
                {
                    GroupName = model.GroupName,
                    Description = model.Description,
                    GroupImage = model.GroupImage,
                    CoverImage = model.CoverImage,
                    CreatedBy = userId,
                    IsPrivate = model.IsPrivate,
                    CreatedDate = DateTimeOffset.UtcNow
                },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<GroupDto> GetGroup(int groupId, int currentUserId)
        {
            return await _dbProvider.ExecuteFirstAsync<GroupDto>(
                "group.sp_Group_GetById",
                new { GroupId = groupId, CurrentUserId = currentUserId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> GetUserGroups(int userId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "group.sp_Group_GetByUser",
                new { UserId = userId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<object> SearchGroups(string searchTerm, int currentUserId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "group.sp_Group_Search",
                new { SearchTerm = searchTerm, CurrentUserId = currentUserId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> JoinGroup(int groupId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "group.sp_Group_AddMember",
                new { GroupId = groupId, UserId = userId, JoinedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> LeaveGroup(int groupId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "group.sp_Group_RemoveMember",
                new { GroupId = groupId, UserId = userId },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<object> GetGroupMembers(int groupId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "group.sp_Group_GetMembers",
                new { GroupId = groupId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }

        public async Task<bool> UpdateGroup(int groupId, int userId, CreateGroupRequestDto model)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "group.sp_Group_Update",
                new
                {
                    GroupId = groupId,
                    UserId = userId,
                    GroupName = model.GroupName,
                    Description = model.Description,
                    GroupImage = model.GroupImage,
                    CoverImage = model.CoverImage,
                    IsPrivate = model.IsPrivate,
                    ModifiedDate = DateTimeOffset.UtcNow
                },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteGroup(int groupId, int userId)
        {
            var rowsAffected = await _dbProvider.ExecuteAsync(
                "group.sp_Group_Delete",
                new { GroupId = groupId, UserId = userId, DeletedDate = DateTimeOffset.UtcNow },
                System.Data.CommandType.StoredProcedure,
                Region.Main);

            return rowsAffected > 0;
        }

        public async Task<object> GetTrendingGroups(int currentUserId, int page, int pageSize)
        {
            return await _dbProvider.ExecuteListAsync(
                "group.sp_Group_GetTrending",
                new { CurrentUserId = currentUserId, Page = page, PageSize = pageSize },
                System.Data.CommandType.StoredProcedure,
                Region.Main);
        }
    }
}
