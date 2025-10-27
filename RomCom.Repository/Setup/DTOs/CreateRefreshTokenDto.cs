using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class CreateRefreshTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

