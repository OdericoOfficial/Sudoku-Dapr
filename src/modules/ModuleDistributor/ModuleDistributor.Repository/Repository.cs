using Microsoft.EntityFrameworkCore;
using ModuleDistributor.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDistributor.Repository
{
    internal class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public void Add(TEntity entity)
            => _context.Set<TEntity>().Add(entity);

        public async ValueTask AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void AddRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().AddRange(entities);

        public async ValueTask AddRangeAsync(IEnumerable<TEntity> entities)
            => await _context.Set<TEntity>().AddRangeAsync(entities);

        public void Delete(TEntity entity)
            => _context.Entry(entity).State = EntityState.Deleted;

        public bool EnsureChanges()
            => _context.SaveChanges() > 0;

        public async ValueTask<bool> EnsureChangesAsync()
            => await _context.SaveChangesAsync() > 0;

        public void Update(TEntity entity)
            => _context.Entry(entity).State = EntityState.Modified;

        public override void Dispose()
        {
            EnsureChanges();
            _context.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await EnsureChangesAsync();
            await _context.DisposeAsync();
        }
    }
}
