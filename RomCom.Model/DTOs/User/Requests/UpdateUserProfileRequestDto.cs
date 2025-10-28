using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.User.Requests
{
    public class UpdateUserProfileRequestDto
    {
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string? FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string? LastName { get; set; }

        [StringLength(500, ErrorMessage = "Profile picture URL cannot exceed 500 characters")]
        public string? ProfilePicture { get; set; }

        [StringLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
        public string? Bio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(20, ErrorMessage = "Gender cannot exceed 20 characters")]
        public string? Gender { get; set; }

        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters")]
        public string? Location { get; set; }

        [StringLength(500, ErrorMessage = "Website URL cannot exceed 500 characters")]
        [Url(ErrorMessage = "Invalid website URL format")]
        public string? Website { get; set; }

        [StringLength(500, ErrorMessage = "Cover image URL cannot exceed 500 characters")]
        public string? CoverImage { get; set; }

        public bool? IsPrivate { get; set; }
    }
}
