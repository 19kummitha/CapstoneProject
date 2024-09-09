using System.ComponentModel.DataAnnotations;

namespace CommunityConnect.Models
{
    public class Post
    {
        public int Id { get; set; } 
        public string Title { get; set; } 

        [Required]
        public string Content { get; set; } 

        public string MediaUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
