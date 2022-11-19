namespace GitBucket.Models.ViewModels
{
    public class PullRequestViewModel : LoggedUserIdModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? TargetRepository { get; set; }
        public string? TargetRepositoryName { get; set; }
        public string? TargetRepositoryContent { get; set; }
        public string UserId { get; set; }
        public string? Source { get; set; }
        public string? SourceName { get; set; }
        public string? SourceContent { get; set; }

        public CommentsViewModel Comments { get; set;}

    }
}
