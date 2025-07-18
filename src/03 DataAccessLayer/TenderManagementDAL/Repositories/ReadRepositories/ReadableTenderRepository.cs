﻿using Dapper;
using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableTenderRepository(IDbConnection connection, string tableName)
    : ReadRepository<Tender, int>(connection, tableName), IReadableTenderRepository
{
    private readonly string _tableName = tableName;
    private readonly IDbConnection _connection = connection;

    public Tender? GetByTitle(string title)
    {
        var sql = $"SELECT * FROM {_tableName} WHERE Title = @title";
        return _connection.QuerySingleOrDefault<Tender>(sql, new { Title = title });
    }
}