using System;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.Infrastructure.Common
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
    }
}
