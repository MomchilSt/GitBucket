using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class IssueInputViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RepositoryId { get; set; }
    }
}
