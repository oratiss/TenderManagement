using System.Data;
using Dapper;

namespace TenderManagementDAL.Repositories.Abstractions
{
    public abstract class ReadRepository<TEntity, TId>(IDbConnection connection) : IReadRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        protected readonly IDbConnection Connection = connection;

        protected abstract string TableName { get; }

        public TEntity? GetById(TId id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
            return Connection.QuerySingleOrDefault<TEntity>(sql, new { Id = id });
        }

        public Task<TEntity?> GetByIdAsync(TId id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
            return Connection.QuerySingleOrDefaultAsync<TEntity>(sql, new { Id = id });
        }

        public (IEnumerable<TEntity>?, long) GetAll(string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";
            var sql = $"""
                       SELECT * FROM {TableName} ORDER BY {orderBy} {order};
                       SELECT COUNT(*) FROM {TableName};
                       """;

            using var multi = Connection.QueryMultiple(sql);
            var data = multi.Read<TEntity>();
            var count = multi.ReadSingle<long>();
            return (data, count);
        }

        public (IEnumerable<TEntity>?, long) GetAll(Dictionary<string, object>? filters, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";
            var sql = $"""
                   SELECT * FROM {TableName} {whereClause} ORDER BY {orderBy} {order};
                   SELECT COUNT(*) FROM {TableName} {whereClause};
                   """;

            using var multi = Connection.QueryMultiple(sql, parameters);
            var data = multi.Read<TEntity>();
            var count = multi.ReadSingle<long>();
            return (data, count);
        }

        public Task<(IEnumerable<TEntity>?, long)> GetAllAsync(string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
            => GetAllAsync(null, orderBy, sortOrder);

        public async Task<(IEnumerable<TEntity>?, long)> GetAllAsync(Dictionary<string, object>? filters, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";
            var sql = $"""
                   SELECT * FROM {TableName} ORDER BY {orderBy} {order};
                   SELECT COUNT(*) FROM {TableName};
                   """;

            await using var multi = await Connection.QueryMultipleAsync(sql);
            var data = await multi.ReadAsync<TEntity>();
            var count = await multi.ReadSingleAsync<long>();
            return (data, count);
        }

        public (IEnumerable<TEntity>?, long) GetPaged(int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var offset = (pageIndex - 1) * pageSize;
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            var sql = $"""
                       SELECT * FROM {TableName}
                       ORDER BY {orderBy} {order}
                       OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                       SELECT COUNT(*) FROM {TableName};
                       """;

            using var multi = Connection.QueryMultiple(sql, parameters);
            var data = multi.Read<TEntity>();
            var count = multi.ReadSingle<long>();
            return (data, count);
        }

        public (IEnumerable<TEntity>?, long) GetPaged(Dictionary<string, object>? filters, int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var offset = (pageIndex - 1) * pageSize;
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";

            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            var sql = $"""
                   SELECT * FROM {TableName}
                   {whereClause}
                   ORDER BY {orderBy} {order}
                   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                   SELECT COUNT(*) FROM {TableName} {whereClause};
                   """;

            using var multi = Connection.QueryMultiple(sql, parameters);
            var data = multi.Read<TEntity>();
            var count = multi.ReadSingle<long>();
            return (data, count);
        }

        public async Task<(IEnumerable<TEntity>?, long)> GetPagedAsync(int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var offset = (pageIndex - 1) * pageSize;
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            var sql = $"""
                       SELECT * FROM {TableName}
                       ORDER BY {orderBy} {order}
                       OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                       SELECT COUNT(*) FROM {TableName};
                       """;

            await using var multi = await Connection.QueryMultipleAsync(sql, parameters);
            var data = await multi.ReadAsync<TEntity>();
            var count = await multi.ReadSingleAsync<long>();
            return (data, count);
        }

        public async Task<(IEnumerable<TEntity>?, long)> GetPagedAsync(Dictionary<string, object>? filters, int pageSize = 10, int pageIndex = 1, string orderBy = "Id", SortOrder sortOrder = SortOrder.Asc)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var offset = (pageIndex - 1) * pageSize;
            var order = sortOrder == SortOrder.Asc ? "ASC" : "DESC";

            parameters.Add("Offset", offset);
            parameters.Add("PageSize", pageSize);

            var sql = $"""
                   SELECT * FROM {TableName}
                   {whereClause}
                   ORDER BY {orderBy} {order}
                   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                   SELECT COUNT(*) FROM {TableName} {whereClause};
                   """;

            await using var multi = await Connection.QueryMultipleAsync(sql, parameters);
            var data = await multi.ReadAsync<TEntity>();
            var count = await multi.ReadSingleAsync<long>();
            return (data, count);
        }

        public TEntity? GetSingleOrDefault(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT * FROM {TableName} {whereClause}";
            return Connection.QuerySingleOrDefault<TEntity>(sql, parameters);
        }

        public Task<TEntity?> GetSingleOrDefaultAsync(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT * FROM {TableName} {whereClause}";
            return Connection.QuerySingleOrDefaultAsync<TEntity>(sql, parameters);
        }

        public TEntity? GetFirstOrDefault(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT TOP 1 * FROM {TableName} {whereClause} ORDER BY Id ASC";
            return Connection.QueryFirstOrDefault<TEntity>(sql, parameters);
        }

        public Task<TEntity?> GetFirstOrDefaultAsync(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT TOP 1 * FROM {TableName} {whereClause} ORDER BY Id ASC";
            return Connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters);
        }

        public TEntity? GetLastOrDefault(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT TOP 1 * FROM {TableName} {whereClause} ORDER BY Id DESC";
            return Connection.QueryFirstOrDefault<TEntity>(sql, parameters);
        }

        public Task<TEntity?> GetLastOrDefaultAsync(Dictionary<string, object> filters)
        {
            var (whereClause, parameters) = BuildWhereClause(filters);
            var sql = $"SELECT TOP 1 * FROM {TableName} {whereClause} ORDER BY Id DESC";
            return Connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters);
        }

        private (string Sql, DynamicParameters Params) BuildWhereClause(Dictionary<string, object>? filters)
        {
            if (filters == null || filters.Count == 0)
                return ("", new DynamicParameters());

            var clauses = new List<string>();
            var parameters = new DynamicParameters();

            foreach (var filter in filters)
            {
                var paramName = $"@{filter.Key}";
                clauses.Add($"{filter.Key} = {paramName}");
                parameters.Add(filter.Key, filter.Value);
            }

            string whereClause = "WHERE " + string.Join(" AND ", clauses);
            return (whereClause, parameters);
        }
    }


    public abstract class ReadRepository<TEntity>(IDbConnection connection) : ReadRepository<TEntity, int>(connection)
        where TEntity : class, IEntity<int>;
}
