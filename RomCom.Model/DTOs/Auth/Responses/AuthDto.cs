using System;

namespace RomCom.Model.DTOs.Auth.Responses
{
    public class AuthDto
    {
        public required int id { get; set; }
        public required string userName { get; set; }
        public required string email { get; set; }
        public required int roleId { get; set; }
        public required string roleName { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? profilePicture { get; set; }
        public DateTimeOffset? lastLoginDate { get; set; }
    }
}

