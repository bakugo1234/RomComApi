using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Comment.Requests
{
    public class CreateCommentRequestDto
    {
        [Required(ErrorMessage = "Post ID is required")]
        public required int PostId { get; set; }

        [Required(ErrorMessage = "Comment content is required")]
        [StringLength(1000, ErrorMessage = "Comment cannot exceed 1000 characters")]
        public required string Content { get; set; }

        public int? ParentCommentId { get; set; } // For nested replies
    }
}
