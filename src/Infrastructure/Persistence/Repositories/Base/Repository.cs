using MessagingSystemApp.Application.Abstracts.Repositories.Base;
using MessagingSystemApp.Domain.Common;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace MessagingSystemApp.Infrastructure.Persistence.Repositories.Base
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public async Task AddAsync(TEntity entity)
        {
            EntityEntry newEntity = await Table.AddAsync(entity);
            newEntity.State = EntityState.Added;
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
        }

        public async Task<TResult?> GetWithSelectAsync<TResult>(
            Expression<Func<TEntity,TResult>> select,
            Expression<Func<TEntity, bool>>? predicate = null, 
            bool tracking = true, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? await query.Select(select).FirstOrDefaultAsync()
                : await query.Where(predicate).Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetAllWithSelectAsync<TOrderBy, TResult>(Expression<Func<TEntity, TOrderBy>> orderBy, Expression<Func<TEntity, TResult>> select, Expression<Func<TEntity, bool>>? predicate = null, bool isOrderBy = true, bool tracking = true, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? isOrderBy
                    ? await query.Select(select).ToListAsync()
                    : await query.OrderByDescending(orderBy).Select(select).ToListAsync()
                : isOrderBy
                    ? await query.Where(predicate).Select(select).ToListAsync()
                    : await query.Where(predicate).OrderByDescending(orderBy).Select(select).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>>? predicate = null, bool tracking = true, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            if (!tracking)
                query = Table.AsNoTracking();
            return predicate is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQuery(params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, Expression<Func<TEntity, bool>>? select = null)
        {
            return select == null ? await Table.AnyAsync(exp) : await Table.Where(select).AnyAsync(exp);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            EntityEntry newEntity = Table.Update(entity);
            newEntity.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            EntityEntry entry = Table.Remove(entity);
            entry.State = EntityState.Deleted;
        }
    }
}
