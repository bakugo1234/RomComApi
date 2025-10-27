using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class UpdatePasswordDto
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}

