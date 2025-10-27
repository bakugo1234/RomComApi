using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.ViewModels
{
    public class UserCredentials
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}

