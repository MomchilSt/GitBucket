using GitBucket.Models;

namespace GitBucket.Data.Repositories.Interfaces
{
    public interface IIssueRepository : IGitRepository<Issue>
    {
        void Update(Issue issue);
    }
}
