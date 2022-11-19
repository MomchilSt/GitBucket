namespace GitBucket.Models.ViewModels
{
    public class RepositoriesViewModel : LoggedUserIdModel
    {
        public IEnumerable<Repository> Repositories { get; set; }

        public IEnumerable<Commit> Commits { get; set; }
    }
}
