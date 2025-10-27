using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class RefreshTokenDto
    {
        public required int RefreshTokenId { get; set; }
        public required int UserId { get; set; }
        public required string Token { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
        public required bool IsRevoked { get; set; }
        public DateTimeOffset? RevokedDate { get; set; }
    }
}

