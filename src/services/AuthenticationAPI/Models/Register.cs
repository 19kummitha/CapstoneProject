using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Models
{
    public class Register:IdentityUser
    {
        public string? FlatNo { get; set; }
        public string? Name { get; set; }
        public string? ServiceType {  get; set; }
    }
}
