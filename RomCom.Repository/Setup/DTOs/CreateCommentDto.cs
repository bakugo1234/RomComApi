using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class CreateCommentDto
    {
        public required int PostId { get; set; }
        public required int UserId { get; set; }
        public required string Content { get; set; }
        public int? ParentCommentId { get; set; }
        public required DateTime CreatedDate { get; set; }
    }
}
