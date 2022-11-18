using GitBucket.Models;

namespace GitBucket.Data.Repositories.Interfaces
{
    public interface ICommentRepository : IGitRepository<Comment>
    {
        void Update(Comment comment);
    }
}
