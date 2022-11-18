namespace GitBucket.Models.ViewModels
{
    public class IssueViewModel
    {
        public int? RepoId { get; set; }
        public IEnumerable<Issue> Issues { get; set;}
    }
}
