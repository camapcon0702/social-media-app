using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("posts")]
    public class Post
    {
        public int Id { get; set; }
        public string? caption { get; set; }
        public string? image { get; set; }
        public string? video { get; set; }
        public User? user { get; set; }
        public List<User> liked { get; set; } = new List<User>();
        public List<User> saved { get; set; } = new List<User>();
        public DateTime? createAt { get; set; }
        public List<Comment> comments { get; set; } = new List<Comment>();
    }
}
