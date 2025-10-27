using System;

namespace RomCom.Model.ViewModels
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public AuthViewModel User { get; set; }
    }
}

