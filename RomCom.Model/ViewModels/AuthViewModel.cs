using System;

namespace RomCom.Model.ViewModels
{
    public class AuthViewModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string profilePicture { get; set; }
        public DateTime? lastLoginDate { get; set; }
    }
}

