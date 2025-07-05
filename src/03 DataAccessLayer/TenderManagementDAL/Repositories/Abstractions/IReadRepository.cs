namespace TenderManagementDAL.Repositories.Abstractions;

public interface IReadRepository<TEntity, TId> where TEntity : class, IEntity<TId>
{
    TEntity? GetById(TId id);
    Task<TEntity?> GetByIdAsync(TId id);
    (IEnumerable<TEntity>?, long) GetAll(string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    (IEnumerable<TEntity>?, long) GetAll(Dictionary<string, object>? filters, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    Task<(IEnumerable<TEntity>?, long)> GetAllAsync(string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    Task<(IEnumerable<TEntity>?, long)> GetAllAsync(Dictionary<string, object>? filters, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    (IEnumerable<TEntity>?, long) GetPaged(int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    (IEnumerable<TEntity>?, long) GetPaged(Dictionary<string, object>? filters, int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    Task<(IEnumerable<TEntity>?, long)> GetPagedAsync(int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    Task<(IEnumerable<TEntity>?, long)> GetPagedAsync(Dictionary<string, object>? filters, int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc);
    TEntity? GetSingleOrDefault(Dictionary<string, object> filters);
    Task<TEntity?> GetSingleOrDefaultAsync(Dictionary<string, object> filters);
    TEntity? GetFirstOrDefault(Dictionary<string, object> filters);
    Task<TEntity?> GetFirstOrDefaultAsync(Dictionary<string, object> filters);
    TEntity? GetLastOrDefault(Dictionary<string, object> filters);
    Task<TEntity?> GetLastOrDefaultAsync(Dictionary<string, object> filters);
}