using System.Linq.Expressions;
using THY.GatePlanner.Model.Entities;

namespace THY.GatePlanner.Infrastructure.Persistence.Repositories
{
    public interface IRepository<T> : IDisposable where T : Base
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T? GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
    }
}
