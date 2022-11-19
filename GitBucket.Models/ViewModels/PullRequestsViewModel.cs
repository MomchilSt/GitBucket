namespace GitBucket.Models.ViewModels
{
    public class PullRequestsViewModel : LoggedUserIdModel
    {
        public string? RepoId { get; set; }

        public IEnumerable<PullRequest> PullRequests { get; set;}

        public IEnumerable<Repository> Repositories { get; set; }
    }
}
