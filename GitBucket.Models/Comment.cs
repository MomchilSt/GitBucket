using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GitBucket.Models
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string PullRequestId { get; set; }
        public PullRequest PullRequest { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
