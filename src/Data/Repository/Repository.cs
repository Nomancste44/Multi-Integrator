using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZohoToInsightIntegrator.Contract.Contracts;

namespace ZohoToInsightIntegrator.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;
        protected readonly Microsoft.EntityFrameworkCore.DbContext Context;

        public Repository(Microsoft.EntityFrameworkCore.DbContext context)
        {
            Context = context;
            _entities = Context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
            => await _entities.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) 
            => await _entities.ToListAsync(cancellationToken);


        public async Task<IEnumerable<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
           => await _entities.Where(predicate).ToListAsync(cancellationToken);

        public async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _entities.SingleOrDefaultAsync(predicate, cancellationToken);


        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
            => await _entities.AddAsync(entity, cancellationToken);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
            => await _entities.AddRangeAsync(entities, cancellationToken);

        public void Update(TEntity entity)
            => _entities.Update (entity);

        public void UpdateRange(IEnumerable<TEntity> entities)
            => _entities.UpdateRange(entities);

        public void Remove(TEntity entity)
            =>  _entities.Remove(entity);

        public void RemoveRange(IEnumerable<TEntity> entities)
            => _entities.RemoveRange(entities);
    }
}