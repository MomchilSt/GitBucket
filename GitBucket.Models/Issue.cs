using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models
{
    public class Issue
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string RepositoryId { get; set; }
        public Repository Repository { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
