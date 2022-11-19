namespace GitBucket.Models.ViewModels
{
    public  class IssueUpdateViewModel : LoggedUserIdModel
    {
        public string? RepoId { get; set; }
        public string? Id { get; set; }
        public string Title { get; set; }
        public string? RepoName { get; set; }
        public string Content { get; set; }
    }
}
