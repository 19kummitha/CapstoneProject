﻿namespace CommunityConnect.DTO
{
    public class RegisterAdminDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}