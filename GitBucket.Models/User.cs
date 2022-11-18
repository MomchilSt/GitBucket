using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models
{
    public class User
    {
        public User()
        {
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
            this.Issues = new HashSet<Issue>();
            this.PullRequests = new HashSet<PullRequest>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
