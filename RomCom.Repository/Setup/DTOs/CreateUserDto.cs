using System;

namespace RomCom.Repository.Setup.DTOs
{
    public class CreateUserDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public required int RoleId { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
        public required int CreatedBy { get; set; }
    }
}

