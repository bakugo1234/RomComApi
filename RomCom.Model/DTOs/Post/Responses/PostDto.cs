using System;

namespace RomCom.Model.DTOs.Post.Responses
{
    public class PostDto
    {
        public required int PostId { get; set; }
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public string? UserProfilePicture { get; set; }
        public string? Content { get; set; }
        public string? MediaType { get; set; }
        public string? MediaUrl { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SharesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
