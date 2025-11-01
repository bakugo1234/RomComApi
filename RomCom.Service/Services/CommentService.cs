using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RomCom.Common.ServiceInstallers.Attributes;
using RomCom.Model.DTOs.Comment.Requests;
using RomCom.Model.DTOs.Comment.Responses;
using RomCom.Repository.Repositories.Contracts;
using RomCom.Repository.Setup.DTOs;
using RomCom.Service.Data.Model.Contract;
using RomCom.Service.Services.Contracts;
using RomCom.Service.Setup.Global;

namespace RomCom.Service.Services
{
    [ScopedService]
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IConfiguration _configuration;
        private readonly IServiceResult _successResponse;
        private readonly IServiceResult _errorResponse;
        private readonly GlobalLogic _globalLogic;

        public CommentService(ICommentRepository commentRepository,
                            IConfiguration configuration,
                            IServiceResult successResult,
                            IServiceResult errorResult)
        {
            _commentRepository = commentRepository;
            _configuration = configuration;
            _globalLogic = new GlobalLogic();
            _successResponse = _globalLogic.BuildServiceResult(successResult, true);
            _errorResponse = _globalLogic.BuildServiceResult(errorResult, false);
        }

        public async Task<IServiceResult> CreateComment(int userId, CreateCommentRequestDto model)
        {
            try
            {
                // Validation
                if (model.PostId <= 0)
                {
                    _errorResponse.Message = "Invalid post ID";
                    return _errorResponse;
                }

                if (string.IsNullOrWhiteSpace(model.Content))
                {
                    _errorResponse.Message = "Comment content is required";
                    return _errorResponse;
                }

                if (model.Content.Length > 1000)
                {
                    _errorResponse.Message = "Comment cannot exceed 1000 characters";
                    return _errorResponse;
                }

                // If ParentCommentId provided, verify parent exists
                if (model.ParentCommentId.HasValue && model.ParentCommentId.Value > 0)
                {
                    var parentComment = await _commentRepository.GetCommentById(model.ParentCommentId.Value, userId);
                    if (parentComment == null)
                    {
                        _errorResponse.Message = "Parent comment not found";
                        return _errorResponse;
                    }
                }

                // Create comment
                var createDto = new CreateCommentDto
                {
                    PostId = model.PostId,
                    UserId = userId,
                    Content = model.Content.Trim(),
                    ParentCommentId = model.ParentCommentId,
                    CreatedDate = DateTime.UtcNow
                };

                var newCommentId = await _commentRepository.CreateComment(createDto);

                // Get created comment with user info
                var comment = await _commentRepository.GetCommentById(newCommentId, userId);

                _successResponse.ResultData = comment;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to create comment: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> GetCommentsByPost(int postId, int currentUserId, int pageNumber, int pageSize)
        {
            try
            {
                // Validation
                if (postId <= 0)
                {
                    _errorResponse.Message = "Invalid post ID";
                    return _errorResponse;
                }

                if (pageNumber < 1)
                {
                    _errorResponse.Message = "Page number must be at least 1";
                    return _errorResponse;
                }

                if (pageSize < 1 || pageSize > 100)
                {
                    _errorResponse.Message = "Page size must be between 1 and 100";
                    return _errorResponse;
                }

                // Get comments
                var result = await _commentRepository.GetCommentsByPost(postId, currentUserId, pageNumber, pageSize);

                var response = new CommentListResponseDto
                {
                    Comments = result.Comments,
                    TotalCount = result.TotalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                _successResponse.ResultData = response;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to get comments: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> DeleteComment(int commentId, int userId)
        {
            try
            {
                // Validation
                if (commentId <= 0)
                {
                    _errorResponse.Message = "Invalid comment ID";
                    return _errorResponse;
                }

                // Delete comment
                var success = await _commentRepository.DeleteComment(commentId, userId);

                if (!success)
                {
                    _errorResponse.Message = "Comment not found or you don't have permission to delete it";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to delete comment: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> LikeComment(int commentId, int userId)
        {
            try
            {
                // Validation
                if (commentId <= 0)
                {
                    _errorResponse.Message = "Invalid comment ID";
                    return _errorResponse;
                }

                // Like comment
                var success = await _commentRepository.LikeComment(commentId, userId);

                if (!success)
                {
                    _errorResponse.Message = "Failed to like comment. You may have already liked it.";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to like comment: {ex.Message}";
                return _errorResponse;
            }
        }

        public async Task<IServiceResult> UnlikeComment(int commentId, int userId)
        {
            try
            {
                // Validation
                if (commentId <= 0)
                {
                    _errorResponse.Message = "Invalid comment ID";
                    return _errorResponse;
                }

                // Unlike comment
                var success = await _commentRepository.UnlikeComment(commentId, userId);

                if (!success)
                {
                    _errorResponse.Message = "Failed to unlike comment. You may not have liked it.";
                    return _errorResponse;
                }

                _successResponse.ResultData = true;
                return _successResponse;
            }
            catch (Exception ex)
            {
                _errorResponse.Message = $"Failed to unlike comment: {ex.Message}";
                return _errorResponse;
            }
        }
    }
}
