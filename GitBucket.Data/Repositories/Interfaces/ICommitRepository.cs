using GitBucket.Models;

namespace GitBucket.Data.Repositories.Interfaces
{
    public interface ICommitRepository : IGitRepository<Commit>
    {
        void Update(Commit commit);
    }
}
