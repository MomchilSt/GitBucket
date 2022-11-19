using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class PullRequestInputViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? SourceName { get; set; }
        public string? RepoId { get; set; }
        public IEnumerable<Repository>? Repositories { get; set;}

        [Required]
        public string TargetedRepo { get; set; }

        public string? PrId { get; set; }
    }
}
