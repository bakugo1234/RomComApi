using System;

namespace RomCom.Model.DTOs.Auth.Responses
{
    public class TokenResponseDto
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public required AuthDto User { get; set; }
    }
}

