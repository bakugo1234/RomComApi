using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Auth.Requests
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}

