using Microsoft.EntityFrameworkCore;
using ModuleDistributor.Repository.Abstractions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ModuleDistributor.Repository
{
    internal class BasicRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>, IDisposable, IAsyncDisposable
        where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _context;
        
        public BasicRepository(DbContext context)
            => _context = context;

        public long Count()
            => AsQueryable().AsNoTracking().LongCount();

        public async ValueTask<long> CountAsync()
            => await AsQueryable().AsNoTracking().LongCountAsync().ConfigureAwait(false);

        public IQueryable<TEntity> AsQueryable(bool noTracking = false)
            => noTracking ? AsQueryable().AsNoTracking() : AsQueryable();

        protected IQueryable<TEntity> BuildIncludes(params Expression<Func<TEntity, object>>[] includes)
            => includes.Aggregate(AsQueryable(), (current, include) => current.Include(include));

        protected IQueryable<TEntity> AsQueryable()
            => _context.Set<TEntity>().AsQueryable();

        public virtual void Dispose()
            => _context.Dispose();

        public virtual async ValueTask DisposeAsync()
            => await _context.DisposeAsync();
    }
}