using System;

namespace RomCom.Model.DTOs.Group.Responses
{
    public class GroupDto
    {
        public required int GroupId { get; set; }
        public required string GroupName { get; set; }
        public string? Description { get; set; }
        public string? GroupImage { get; set; }
        public string? CoverImage { get; set; }
        public required int CreatedBy { get; set; }
        public required string CreatedByName { get; set; }
        public bool IsPrivate { get; set; }
        public int MembersCount { get; set; }
        public int PostsCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsMember { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
