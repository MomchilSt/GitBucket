namespace GitBucket.Models.ViewModels
{
    public class RepositoriesViewModel
    {
        public IEnumerable<Repository> Repositories { get; set; }

        public IEnumerable<Commit> Commits { get; set; }
    }
}
