using System.ComponentModel.DataAnnotations;

namespace RomCom.Model.DTOs.Auth.Requests
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}

