using System.Linq.Expressions;

namespace GitBucket.Data.Repositories
{
    public interface IGitRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
