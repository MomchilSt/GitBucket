namespace GitBucket.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepoRepository RepoRepository { get; set; }
        ICommitRepository CommitRepository { get; set; }
        ICommentRepository CommentRepository { get; set; }
        IPullRequestRepository PullRequestRepository { get; set; }
        IIssueRepository IssueRepository { get; set; }
        void Save();
    }
}
