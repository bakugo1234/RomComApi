using System;

namespace RomCom.Model.DTOs.Comment.Responses
{
    public class CommentDto
    {
        public required int CommentId { get; set; }
        public required int PostId { get; set; }
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public string? UserProfilePicture { get; set; }
        public required string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
