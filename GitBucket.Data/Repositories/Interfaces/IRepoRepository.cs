using GitBucket.Models;

namespace GitBucket.Data.Repositories.Interfaces
{
    public interface IRepoRepository : IGitRepository<Repository>
    {
        void Update(Repository repo);
    }
}
