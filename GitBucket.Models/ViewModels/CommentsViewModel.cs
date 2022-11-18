namespace GitBucket.Models.ViewModels
{
    public class CommentsViewModel
    {
        public int PrId { get; set; }
        public IEnumerable<Comment> Comments { get; set;}
    }
}
