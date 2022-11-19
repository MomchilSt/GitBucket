using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class IssueInputViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string RepositoryId { get; set; }

        public string? RepoName { get; set; }
    }
}
