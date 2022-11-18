using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;

namespace GitBucket.Data.Repositories
{
    public class CommentRepository : GitRepository<Comment>, ICommentRepository
    {
        private readonly GitBucketDbContext _db;

        public CommentRepository(GitBucketDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Comment comment)
        {
            _db.Comments.Update(comment);
        }
    }
}
