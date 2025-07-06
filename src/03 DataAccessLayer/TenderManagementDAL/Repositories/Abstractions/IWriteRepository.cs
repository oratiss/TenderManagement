using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TenderManagementDAL.Repositories.Abstractions
{
    public interface IWriteRepository<TEntity, TId> where TEntity : class
    {
        IQueryable<TEntity> DataSource { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
        Task AddInBulkAsync(List<TEntity> entities);
        void EditInBulk(List<TEntity> entities);
        void DeleteAllInBulk(List<TId> keys);

    }

    public interface IWriteRepository<TEntity> : IWriteRepository<TEntity, int> where TEntity : class
    {
    }
}
