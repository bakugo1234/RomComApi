using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class CreateRefreshTokenDto
    {
        public required int UserId { get; set; }
        public required string Token { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
    }
}

