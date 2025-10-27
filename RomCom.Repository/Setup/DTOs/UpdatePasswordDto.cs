using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class UpdatePasswordDto
    {
        public required int UserId { get; set; }
        public required string PasswordHash { get; set; }
        public required DateTimeOffset ModifiedDate { get; set; }
        public required int ModifiedBy { get; set; }
    }
}

