using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDistributor.Repository.Abstractions
{
    public interface IRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        void Add(TEntity entity);
        ValueTask AddAsync(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        ValueTask AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        bool EnsureChanges();
        ValueTask<bool> EnsureChangesAsync();
    }
}
