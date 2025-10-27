using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class RefreshTokenDto
    {
        public int RefreshTokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedDate { get; set; }
    }
}

