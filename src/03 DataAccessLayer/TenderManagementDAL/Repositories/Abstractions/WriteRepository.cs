using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TenderManagementDAL.Repositories.Abstractions
{
    public abstract class WriteRepository<TEntity, TId, TContext> : IWriteRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TContext : IEfDataContext
    {
        protected readonly DbSet<TEntity> DbSet;

        protected WriteRepository(IEfDataContext context)
        {
            Context = context;
            DbSet = (DbSet<TEntity>)Context.Set<TEntity>();
        }

        protected IEfDataContext Context { get; set; }

        public IQueryable<TEntity> DataSource => DbSet;

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Edit(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            var @base = entity as ISoftDeleteEnabledBase;
            if (@base is not null)
            {
                @base.IsDeleted = true;
                DbSet.Update(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }
        }

        public void Delete(TId id)
        {
            Delete(GetById(id));
        }

        public async Task AddInBulkAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        public void EditInBulk(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void DeleteAllInBulk(List<TId> keys)
        {
            var entitiesToDelete = keys.Select(GetById).ToList();
            DbSet.RemoveRange(entitiesToDelete);
        }
    }

    public abstract class WriteRepository<TEntity, TDataContext> : WriteRepository<TEntity, int, TDataContext> where TEntity : class, IEntity<int> where TDataContext : IEfDataContext
    {
        protected WriteRepository(IEfDataContext dataContext) : base(dataContext)
        {
        }
    }
}
