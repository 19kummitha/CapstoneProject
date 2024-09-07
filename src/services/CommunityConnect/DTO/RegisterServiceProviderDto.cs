namespace CommunityConnect.DTO
{
    public class RegisterServiceProviderDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ServiceType { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
