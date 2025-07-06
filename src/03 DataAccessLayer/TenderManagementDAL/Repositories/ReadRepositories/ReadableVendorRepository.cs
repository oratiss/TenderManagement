using Dapper;
using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableVendorRepository(IDbConnection connection, string tableName)
    : ReadRepository<Vendor, int>(connection, tableName), IReadableVendorRepository
{
    private readonly IDbConnection _connection = connection;
    private readonly string _tableName = tableName;

    public Vendor? GetByTitle(string requestTitle)
    {
        var sql = $"SELECT * FROM {_tableName} WHERE Title = @title";
        return _connection.QuerySingleOrDefault<Vendor>(sql, new { Title = requestTitle });
    }

    public async Task<Vendor?> GetByTitleAsync(string requestTitle)
    {
        var sql = $"SELECT * FROM {_tableName} WHERE Title = @title";
        return await _connection.QuerySingleOrDefaultAsync<Vendor>(sql, new { Title = requestTitle });
    }
}