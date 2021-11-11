using ELibrary.Core;
using System.Linq.Expressions;

namespace ELibrary.Data.Contract
{
    public interface ICoreRepo<T> where T : Entity
    {
        IQueryable<T> GetAll();

        T Get(object id);
        IQueryable<T> GetAllWithDelete();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);

        void Remove(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
