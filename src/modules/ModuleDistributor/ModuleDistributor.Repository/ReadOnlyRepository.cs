using Microsoft.EntityFrameworkCore;
using ModuleDistributor.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDistributor.Repository
{
    internal class ReadOnlyRepository<TEntity, TKey> : BasicRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        public ReadOnlyRepository(DbContext context) : base(context)
        {
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
            => AsQueryable().AsNoTracking().Any(predicate);

        public async ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
            => await AsQueryable().AsNoTracking().AnyAsync(predicate);

        public TEntity? FirstOrDefault(TKey key)
            => AsQueryable().AsNoTracking().FirstOrDefault(item => item.Id.Equals(key));

        public async ValueTask<TEntity?> FirstOrDefaultAsync(TKey key)
            => await AsQueryable().AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(key));

        public IEnumerable<TEntity> GetAll()
            => AsQueryable().AsNoTracking().ToList();

        public async ValueTask<IEnumerable<TEntity>> GetAllAsync()
            => await AsQueryable().AsNoTracking().ToListAsync();

        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
            => BuildIncludes(includes).ToList();

        public async ValueTask<IEnumerable<TEntity>> IncludeAsync(params Expression<Func<TEntity, object>>[] includes)
            => await BuildIncludes(includes).ToListAsync();

        public TEntity? IncludeFirstOrDefault(TKey key, params Expression<Func<TEntity, object>>[] includes)
            => BuildIncludes(includes).FirstOrDefault(item => item.Id.Equals(key));

        public async ValueTask<TEntity?> IncludeFirstOrDefaultAsync(TKey key, params Expression<Func<TEntity, object>>[] includes)
            => await BuildIncludes(includes).FirstOrDefaultAsync(item => item.Id.Equals(key));

        public IEnumerable<TType> IncludeSelect<TType>(Expression<Func<TEntity, TType>> select, params Expression<Func<TEntity, object>>[] includes) where TType : class
            => BuildIncludes(includes).Select(select).ToList();

        public async ValueTask<IEnumerable<TType>> IncludeSelectAsync<TType>(Expression<Func<TEntity, TType>> select, params Expression<Func<TEntity, object>>[] includes) where TType : class
            => await BuildIncludes(includes).Select(select).ToListAsync();

        public IEnumerable<TEntity> IncludeWhere(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
            => BuildIncludes(includes).Where(predicate).ToList();

        public async ValueTask<IEnumerable<TEntity>> IncludeWhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
            => await BuildIncludes(includes).Where(predicate).ToListAsync();

        public IEnumerable<TType> IncludeWhereSelect<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TType : class
            => BuildIncludes(includes).Where(predicate).Select(select).ToList();

        public async ValueTask<IEnumerable<TType>> IncludeWhereSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TType : class
            => await BuildIncludes(includes).Where(predicate).Select(select).ToListAsync();

        public TEntity? InculdeFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
            => BuildIncludes(includes).FirstOrDefault(predicate);

        public async ValueTask<TEntity?> InculdeFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
            => await BuildIncludes(includes).FirstOrDefaultAsync(predicate);

        public IEnumerable<TType> Select<TType>(Expression<Func<TEntity, TType>> select) where TType : class
            => AsQueryable().AsNoTracking().Select(select).ToList();

        public async ValueTask<IEnumerable<TType>> SelectAsync<TType>(Expression<Func<TEntity, TType>> select) where TType : class
            => await AsQueryable().AsNoTracking().Select(select).ToListAsync();

        public IEnumerable<TType> WhereSelect<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate) where TType : class
            => AsQueryable().AsNoTracking().Where(predicate).Select(select).ToList();

        public async ValueTask<IEnumerable<TType>> WhereSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate) where TType : class
            => await AsQueryable().AsNoTracking().Where(predicate).Select(select).ToListAsync();

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
            => AsQueryable().AsNoTracking().Where(predicate).ToList();

        public async ValueTask<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
            => await AsQueryable().AsNoTracking().Where(predicate).ToListAsync();

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => AsQueryable().AsNoTracking().FirstOrDefault(predicate);

        public async ValueTask<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
            => await AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
    }
}
