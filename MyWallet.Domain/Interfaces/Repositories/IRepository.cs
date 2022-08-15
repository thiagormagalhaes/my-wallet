using System.Linq.Expressions;

namespace MyWallet.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity entity);
        Task AddRange(IList<TEntity> entities);
        Task<IList<TEntity>> GetAll();
        Task<TEntity?> GetById(long id);
        Task<IQueryable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression);
        Task Update(TEntity entity);
        Task Remove(params long[] ids);
        Task Remove(TEntity entity);
        Task RemoveAll();
        Task SaveChanges();
    }
}
