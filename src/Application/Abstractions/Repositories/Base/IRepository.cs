using MessagingSystemApp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstracts.Repositories.Base
{
    public interface IRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public DbSet<TEntity> Table { get; }
        Task AddAsync(TEntity entity);
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes);
        Task<TResult?> GetWithSelectAsync<TResult>(
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? predicate = null,
        bool tracking = true,
        params string[] includes);
        Task<List<TResult>> GetAllWithSelectAsync<TOrderBy, TResult>(
        Expression<Func<TEntity, TOrderBy>> orderBy,
        Expression<Func<TEntity, TResult>> select,
        Expression<Func<TEntity, bool>>? predicate = null,
        bool isOrderBy = true,
        bool tracking = true,
        params string[] includes);
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp);
        public void Update(TEntity entity);
        void Remove(TEntity entity);
        IQueryable<TEntity> GetQuery(params string[] includes);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
