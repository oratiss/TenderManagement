using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableStatusRepository(IDbConnection connection, string tableName)
    : ReadRepository<Status, long>(connection, tableName), IReadableStatusRepository
{
}