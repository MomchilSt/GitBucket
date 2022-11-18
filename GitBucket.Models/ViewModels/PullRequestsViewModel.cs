namespace GitBucket.Models.ViewModels
{
    public  class PullRequestsViewModel
    {
        public int? RepoId { get; set; }

        public IEnumerable<PullRequest> PullRequests { get; set;}
    }
}
