namespace GitBucket.Models.ViewModels
{
    public class CommentsViewModel : LoggedUserIdModel
    {
        public string PrId { get; set; }

        public string TargetRepo { get; set; }
        public IEnumerable<Comment> Comments { get; set;}

        public IEnumerable<Repository> Repositories { get; set; }
    }
}
