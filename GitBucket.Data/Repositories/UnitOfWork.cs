using GitBucket.Data.Repositories.Interfaces;

namespace GitBucket.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepoRepository RepoRepository { get; set; }

        public ICommitRepository CommitRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }

        public IPullRequestRepository PullRequestRepository { get; set; }
        public IIssueRepository IssueRepository { get; set; }

        private readonly GitBucketDbContext _db;

        public UnitOfWork(GitBucketDbContext db)
        {
            _db = db;
            this.RepoRepository = new RepoRepository(db);
            this.CommitRepository = new CommitRepository(db);
            this.PullRequestRepository = new PullRequestRepository(db);
            this.IssueRepository = new IssueRepository(db);
            this.CommentRepository = new CommentRepository(db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
