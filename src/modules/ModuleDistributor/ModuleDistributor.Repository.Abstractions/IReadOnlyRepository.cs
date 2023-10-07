using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDistributor.Repository.Abstractions
{
    public interface IReadOnlyRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> GetAll();
        ValueTask<IEnumerable<TEntity>> GetAllAsync();

        IEnumerable<TType> Select<TType>(Expression<Func<TEntity, TType>> select) where TType : class;
        ValueTask<IEnumerable<TType>> SelectAsync<TType>(Expression<Func<TEntity, TType>> select) where TType : class;

        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        ValueTask<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        
        IEnumerable<TType> WhereSelect<TType>(Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> predicate) where TType : class;
        ValueTask<IEnumerable<TType>> WhereSelectAsync<TType>(Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> predicate) where TType : class;
        
        IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        ValueTask<IEnumerable<TEntity>> IncludeAsync(params Expression<Func<TEntity, object>>[] includes);
        
        IEnumerable<TType> IncludeSelect<TType>(Expression<Func<TEntity, TType>> select,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;
        ValueTask<IEnumerable<TType>> IncludeSelectAsync<TType>(Expression<Func<TEntity, TType>> select,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;

        IEnumerable<TEntity> IncludeWhere(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);
        ValueTask<IEnumerable<TEntity>> IncludeWhereAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TType> IncludeWhereSelect<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;
        ValueTask<IEnumerable<TType>> IncludeWhereSelectAsync<TType>(Expression<Func<TEntity, TType>> select, Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes) where TType : class;
        
        TEntity? FirstOrDefault(TKey key);
        ValueTask<TEntity?> FirstOrDefaultAsync(TKey key);

        TEntity? IncludeFirstOrDefault(TKey key, params Expression<Func<TEntity, object>>[] includes);
        ValueTask<TEntity?> IncludeFirstOrDefaultAsync(TKey key, params Expression<Func<TEntity, object>>[] includes);

        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        ValueTask<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity? InculdeFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        ValueTask<TEntity?> InculdeFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes);

        bool Any(Expression<Func<TEntity, bool>> predicate);
        ValueTask<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
