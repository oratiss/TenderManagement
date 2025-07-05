using System.Data;
using TenderManagementDAL.Models;
using TenderManagementDAL.Repositories.Abstractions;

namespace TenderManagementDAL.Repositories.ReadRepositories;

public class ReadableCategoryRepository(IDbConnection connection, string tableName)
    : ReadRepository<Category, int>(connection, tableName), IReadableCategoryRepository
{
}