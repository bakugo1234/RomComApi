using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Auth.Requests
{
    public class ChangePasswordRequestDto
    {
        [Required]
        public required string OldPassword { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public required string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
    }
}

