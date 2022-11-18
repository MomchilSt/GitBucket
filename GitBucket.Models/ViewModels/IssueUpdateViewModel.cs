namespace GitBucket.Models.ViewModels
{
    public  class IssueUpdateViewModel
    {
        public int? RepoId { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string? RepoName { get; set; }
        public string Content { get; set; }
    }
}
