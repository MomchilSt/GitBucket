using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GitBucket.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Repositories = new HashSet<Repository>();
            this.Commits = new HashSet<Commit>();
            this.Issues = new HashSet<Issue>();
            this.PullRequests = new HashSet<PullRequest>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Repository> Repositories { get; set; }

        public ICollection<Commit> Commits { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<PullRequest> PullRequests { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
