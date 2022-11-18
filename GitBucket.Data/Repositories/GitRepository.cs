using GitBucket.Data.Repositories.Interfaces;
using GitBucket.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GitBucket.Data.Repositories
{
    public class GitRepository<T> : IGitRepository<T> where T : class
    {
        private readonly GitBucketDbContext _db;
        internal DbSet<T> dbSet { get; set; }

        public IEnumerable<T> GetAll() 
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }


        public GitRepository(GitBucketDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
