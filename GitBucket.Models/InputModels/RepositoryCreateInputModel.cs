using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.InputModels
{
    public class RepositoryCreateInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Access { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Name should be 3 characters!")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Content should be 10 characters!")]
        public string Content { get; set; }
    }
}
