using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;
using MyWallet.Domain.Interfaces.Repositories;
using MyWallet.Infra.Data.Context;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace MyWallet.Infra.Repositories
{
    public class Repository<TEntity, T> : IRepository<TEntity, T> where TEntity : Entity<T>
    {
        protected MyWalletContext _context;
        protected DbSet<TEntity> _set;

        public Repository(MyWalletContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _set.AddAsync(entity);
            await SaveChanges();
        }

        public async Task AddRange(IList<TEntity> entities)
        {
            await _set.AddRangeAsync(entities);
            await SaveChanges();
        }

        public virtual async Task<IList<TEntity>> GetAll()
        {
            return await _set.Select(x => x).ToListAsync();
        }

        public virtual async Task<TEntity?> GetById(T id)
        {
            return await _set.FindAsync(id);
        }

        public virtual async Task<IQueryable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
        {
            return await Task.FromResult(_set.Where(filterExpression));
        }

        public async Task Remove(params T[] ids)
        {
            _set.Where(d => ids.Contains(d.Id)).Delete();
            await SaveChanges();
        }

        public async Task Remove(TEntity entity)
        {
            _set.Remove(entity);
            await SaveChanges();
        }

        public async Task RemoveAll()
        {
            _set.Delete();
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            var exist = _set.Find(entity.Id);

            if (exist is null)
            {
                return;
            }

            _context.Entry(exist).CurrentValues.SetValues(entity);

            await SaveChanges();
        }

        public async Task Update(IList<TEntity> entities)
        {
            var ids = entities.Select(x => x.Id);

            var exists = _set.Where(x => ids.Contains(x.Id));

            foreach (var exist in exists)
            {
                var entity = entities.First(x => x.Id is not null && x.Id.Equals(exist.Id));

                _context.Entry(exist).CurrentValues.SetValues(entity);
            }

            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
