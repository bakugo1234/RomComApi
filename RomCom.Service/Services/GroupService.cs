using System;
using System.Threading.Tasks;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Group.Requests;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;

        public GroupService(IGroupRepository groupRepository, IServiceResult successResult, IServiceResult errorResult)
        {
            _groupRepository = groupRepository;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> CreateGroup(int userId, CreateGroupRequestDto model)
        {
            try
            {
                var groupId = await _groupRepository.CreateGroup(userId, model);
                if (groupId <= 0)
                {
                    _errorResponse.Message = "Failed to create group";
                    return _errorResponse;
                }

                var createdGroup = await _groupRepository.GetGroup(groupId, userId);
                _successResponse.ResultData = createdGroup;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to create group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetGroup(int groupId, int currentUserId)
        {
            try
            {
                var group = await _groupRepository.GetGroup(groupId, currentUserId);
                if (group == null)
                {
                    _errorResponse.Message = "Group not found";
                    return _errorResponse;
                }

                _successResponse.ResultData = group;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetUserGroups(int userId, int page, int pageSize)
        {
            try
            {
                var groups = await _groupRepository.GetUserGroups(userId, page, pageSize);
                _successResponse.ResultData = groups;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get user groups: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> SearchGroups(string searchTerm, int currentUserId, int page, int pageSize)
        {
            try
            {
                var groups = await _groupRepository.SearchGroups(searchTerm, currentUserId, page, pageSize);
                _successResponse.ResultData = groups;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to search groups: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> JoinGroup(int groupId, int userId)
        {
            try
            {
                var success = await _groupRepository.JoinGroup(groupId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to join group";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to join group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> LeaveGroup(int groupId, int userId)
        {
            try
            {
                var success = await _groupRepository.LeaveGroup(groupId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to leave group";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to leave group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetGroupMembers(int groupId, int page, int pageSize)
        {
            try
            {
                var members = await _groupRepository.GetGroupMembers(groupId, page, pageSize);
                _successResponse.ResultData = members;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get group members: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> UpdateGroup(int groupId, int userId, CreateGroupRequestDto model)
        {
            try
            {
                var success = await _groupRepository.UpdateGroup(groupId, userId, model);
                if (!success)
                {
                    _errorResponse.Message = "Failed to update group or insufficient permissions";
                    return _errorResponse;
                }

                var updatedGroup = await _groupRepository.GetGroup(groupId, userId);
                _successResponse.ResultData = updatedGroup;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to update group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> DeleteGroup(int groupId, int userId)
        {
            try
            {
                var success = await _groupRepository.DeleteGroup(groupId, userId);
                if (!success)
                {
                    _errorResponse.Message = "Failed to delete group or insufficient permissions";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to delete group: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetTrendingGroups(int currentUserId, int page, int pageSize)
        {
            try
            {
                var groups = await _groupRepository.GetTrendingGroups(currentUserId, page, pageSize);
                _successResponse.ResultData = groups;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get trending groups: {ex.Message}";
                return _errorResponse;
            }
        }
    }
}
