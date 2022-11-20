using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class IssueInputViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title should be 3 characters!")]
        public string Title { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Content should be 10 characters!")]
        public string Content { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string RepositoryId { get; set; }

        public string? RepoName { get; set; }
    }
}
