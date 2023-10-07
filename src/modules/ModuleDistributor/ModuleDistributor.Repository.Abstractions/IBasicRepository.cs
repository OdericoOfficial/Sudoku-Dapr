using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDistributor.Repository.Abstractions
{
    public interface IBasicRepository<out TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> AsQueryable(bool noTracking = false);
        long Count();
        ValueTask<long> CountAsync();
    }
}
