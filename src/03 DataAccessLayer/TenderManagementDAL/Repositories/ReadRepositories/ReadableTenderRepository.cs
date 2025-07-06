using Dapper;
using System.Data;
using System.Security.Cryptography;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;
using static Dapper.SqlMapper;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableTenderRepository(IDbConnection connection, string tableName)
    : ReadRepository<Tender, int>(connection, tableName), IReadableTenderRepository
{
    public Tender? GetByTitle(string title)
    {
        var sql = $"SELECT * FROM {tableName} WHERE Title = @title";
        return connection.QuerySingleOrDefault<Tender>(sql, new { Title = title });
    }
}