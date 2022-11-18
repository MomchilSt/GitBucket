using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;

namespace GitBucket.Data.Repositories
{
    public class IssueRepository : GitRepository<Issue>, IIssueRepository
    {
        private readonly GitBucketDbContext _db;

        public IssueRepository(GitBucketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Issue issue)
        {
            _db.Issues.Update(issue);
        }
    }
}
