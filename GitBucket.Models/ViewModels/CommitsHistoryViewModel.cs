namespace GitBucket.Models.ViewModels
{
    public class CommitsHistoryViewModel : LoggedUserIdModel
    {
        public string? RepoId { get; set; }
        public IEnumerable<Commit> CommitsHistory { get; set; }
    }
}
