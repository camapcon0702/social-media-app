using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("messages")]
    public class Message
    {
        [Key] public int Id { get; set; }
        public string? content { get; set; }
        public string? image { get; set; }
        public User? user { get; set; }
        public Chat? chat { get; set; }
        public DateTime timestamp { get; set; }
    }
}
