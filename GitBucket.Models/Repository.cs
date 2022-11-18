using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models
{
    public class Repository
    {
        public Repository()
        {
            this.Commits = new HashSet<Commit>();
            this.Issues = new HashSet<Issue>();
            this.PullRequests = new HashSet<PullRequest>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Access { get; set; }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        public ICollection<Commit> Commits { get; set; }
        public ICollection<Issue> Issues { get; set; }
        public ICollection<PullRequest> PullRequests { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
