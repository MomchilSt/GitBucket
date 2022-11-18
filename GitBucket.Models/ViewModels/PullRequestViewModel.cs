namespace GitBucket.Models.ViewModels
{
    public class PullRequestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TargetRepository { get; set; }
        public string? TargetRepositoryName { get; set; }
        public string? TargetRepositoryContent { get; set; }
        public int UserId { get; set; }
        public int? Source { get; set; }
        public string? SourceName { get; set; }
        public string? SourceContent { get; set; }

        public CommentsViewModel Comments { get; set;}

    }
}
