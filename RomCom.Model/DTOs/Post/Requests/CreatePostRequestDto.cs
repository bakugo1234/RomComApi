using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Post.Requests
{
    public class CreatePostRequestDto
    {
        [StringLength(4000, ErrorMessage = "Content cannot exceed 4000 characters")]
        public string? Content { get; set; }

        [StringLength(50, ErrorMessage = "Media type cannot exceed 50 characters")]
        public string? MediaType { get; set; } // 'image', 'video', 'gif', 'none'

        [StringLength(500, ErrorMessage = "Media URL cannot exceed 500 characters")]
        public string? MediaUrl { get; set; }

        public int? GroupId { get; set; } // Optional: for group posts
    }
}
