using GitBucket.Models;

namespace GitBucket.Data.Repositories.Interfaces
{
    public interface IPullRequestRepository : IGitRepository<PullRequest>
    {
        void Update(PullRequest pr);
    }
}
