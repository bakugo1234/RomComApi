using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Group.Requests
{
    public class CreateGroupRequestDto
    {
        [Required(ErrorMessage = "Group name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Group name must be between 3 and 100 characters")]
        public required string GroupName { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [StringLength(500, ErrorMessage = "Group image URL cannot exceed 500 characters")]
        public string? GroupImage { get; set; }

        [StringLength(500, ErrorMessage = "Cover image URL cannot exceed 500 characters")]
        public string? CoverImage { get; set; }

        public bool IsPrivate { get; set; } = false;
    }
}
