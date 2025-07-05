using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableTenderRepository(IDbConnection connection, string tableName)
    : ReadRepository<Tender, int>(connection, tableName), IReadableTenderRepository
{
}