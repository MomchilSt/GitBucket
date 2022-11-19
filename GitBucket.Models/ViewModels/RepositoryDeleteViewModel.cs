using System.ComponentModel.DataAnnotations;

namespace GitBucket.Models.ViewModels
{
    public class RepositoryDeleteViewModel : LoggedUserIdModel
    {
        public string Id { get; set; }
        public string Access { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
