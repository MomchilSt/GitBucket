using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using System.Linq.Expressions;

namespace GitBucket.Data.Repositories
{
    public class RepoRepository : GitRepository<Repository>, IRepoRepository
    {
        private readonly GitBucketDbContext _db;

        public RepoRepository(GitBucketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Repository repo)
        {
            _db.Repository.Update(repo);
        }
    }
}
