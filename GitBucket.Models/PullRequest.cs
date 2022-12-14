using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models
{
    public class PullRequest
    {
        public PullRequest()
        {
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? TargetRepository { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string? Source { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
