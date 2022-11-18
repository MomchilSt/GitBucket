using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;

namespace GitBucket.Data.Repositories
{
    public class PullRequestRepository  : GitRepository<PullRequest>, IPullRequestRepository
    {
        private readonly GitBucketDbContext _db;

        public PullRequestRepository(GitBucketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PullRequest pr)
        {
            _db.PullRequests.Update(pr);
        }
    }
}
