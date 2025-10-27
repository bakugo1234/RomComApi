using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.ViewModels
{
    public class RefreshTokenModel
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}

