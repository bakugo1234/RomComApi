using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.ViewModels
{
    public class UserCredentials
    {
        [Required(ErrorMessage = "Username or Email is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

