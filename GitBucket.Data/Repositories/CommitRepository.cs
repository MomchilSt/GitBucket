using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;

namespace GitBucket.Data.Repositories
{
    public class CommitRepository : GitRepository<Commit>, ICommitRepository
    {
        private readonly GitBucketDbContext _db;

        public CommitRepository(GitBucketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Commit commit)
        {
            _db.Commits.Update(commit);
        }
    }
}
