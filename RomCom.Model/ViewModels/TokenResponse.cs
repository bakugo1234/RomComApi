using System;

namespace RomCom.Model.ViewModels
{
    public class TokenResponse
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public required AuthViewModel User { get; set; }
    }
}

