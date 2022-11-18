using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class PullRequestInputViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? SourceName { get; set; }
        public int? RepoId { get; set; }
        public IEnumerable<Repository>? Repositories { get; set;}

        [Required]
        public int TargetedRepo { get; set; }

        public int? PrId { get; set; }
    }
}
