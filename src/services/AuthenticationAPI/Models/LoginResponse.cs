namespace AuthenticationAPI.Models
{
    public class LoginResponse:Response
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
