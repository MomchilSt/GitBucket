using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class CommitInputViewmodel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title should be 3 characters!")]
        public string Title { get; set; }
        public string RepoId { get; set; }

        public string? CommitId { get; set; }

        public string? RepoName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Content should be 10 characters!")]
        public string RepoContent { get; set;}
    }
}
