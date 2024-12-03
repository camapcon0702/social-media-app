using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key] public int Id { get; set; }
        public string? content { get; set; }
        public User? user { get; set; }
        public List<User> liked { get; set; } = new List<User>();
        public List<Post> posts { get; set; } = new List<Post>();
        public DateTime createAt { get; set; }
    }
}
