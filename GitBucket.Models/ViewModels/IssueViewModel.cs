namespace GitBucket.Models.ViewModels
{
    public class IssueViewModel : LoggedUserIdModel 
    {
        public string? RepoId { get; set; }
        public IEnumerable<Issue> Issues { get; set;}

        public IEnumerable<Repository> Repositories { get; set; }
    }
}
