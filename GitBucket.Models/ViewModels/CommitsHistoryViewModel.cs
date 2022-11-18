namespace GitBucket.Models.ViewModels
{
    public class CommitsHistoryViewModel
    {
        public int? RepoId { get; set; }
        public IEnumerable<Commit> CommitsHistory { get; set; }
    }
}
